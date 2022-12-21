using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorImage;
        private stats_controller stats;

        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        //public float RotationSmoothTime = 0.5f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.5f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        [Tooltip("Insert Weapons Prefabs")]
        [Header("Weapons")]
        public GameObject Sword;

        [Tooltip("Insert Weapons Effects")]
        [Header("Effects")]
        public ParticleSystem SwordEffect;

        [Tooltip("Insert Weapons AudioClips")]
        [Header("Weapons Sounds")]
        public AudioClip SwordSlashAudioClip;
        public AudioClip SwordSlashHitAudioClip;
        public AudioClip SwordSlashInteractableAudioClip;
        public AudioClip FireAudioClip;
        public AudioClip BlubHurtAudioClip;


        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private float _interactionDistance = 0.8f;
        private Vector3 _rayOrigin;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDCrouch;
        private int _animIDBack;
        private int _animIDRight;
        private int _animIDLeft;
        private int _animIDRightLook;
        private int _animIDLeftLook;
        private int _animIDSword;
        private int _animIDLavaDamage;
        private int _animIDDeath;

        // flags
        private int _hitFlag = 0;
        public bool _gameOver = false;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private const float _threshold = 0.01f;
        private bool _hasAnimator;
        private AudioSource _audioSource;
        

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            //UI Hearts
            stats = transform.parent.GetComponent<stats_controller>();

            Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
            Cursor.visible = false;
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            //Initialize controller height
            _controller.height = 1.45f;
            _controller.center = new Vector3(0, 0.76f, 0);

            //Weapon must be invisible
            Sword.SetActive(false);

            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {   
            _hasAnimator = TryGetComponent(out _animator);
            if (!stats.UI_active && !_gameOver)
            {
                Cursor.lockState = CursorLockMode.Locked;
                if(!_animator.GetBool(_animIDLavaDamage)){
                    Crouch();
                    UseSword();
                    //CheckInteraction();
                }
                else {
                    _animator.SetBool(_animIDCrouch, false);
                }
                JumpAndGravity();
                GroundedCheck();
                Move();
            }
            else if (stats.UI_active){
                Cursor.lockState = CursorLockMode.None;
            }
            else if (_gameOver){
                Cursor.lockState = CursorLockMode.None;
                _animator.SetBool(_animIDCrouch, false);
                _animator.SetBool(_animIDRightLook, false);
                _animator.SetBool(_animIDLeftLook, false);
                _animator.SetBool(_animIDRight, false);
                _animator.SetBool(_animIDLeft, false);
                _animator.SetBool(_animIDBack, false);
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDSword, false);
                _animator.SetBool(_animIDFreeFall, false);

                /*
                _animator.SetBool(_animIDSpeed, 0);
                _animator.SetBool(_animIDGrounded, false);
                _animator.SetBool(_animIDMotionSpeed, 0);
                */
            }
        }

        private void LateUpdate()
        {
            if (!stats.UI_active && !_gameOver)
            {
                CameraRotation();
            }
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDCrouch = Animator.StringToHash("Crouch");
            _animIDBack = Animator.StringToHash("Back");
            _animIDRight = Animator.StringToHash("Right");
            _animIDLeft = Animator.StringToHash("Left");
            _animIDRightLook = Animator.StringToHash("RightLook");
            _animIDLeftLook = Animator.StringToHash("LeftLook");
            _animIDSword = Animator.StringToHash("Sword");
            _animIDLavaDamage = Animator.StringToHash("Lava");
            _animIDDeath = Animator.StringToHash("Death");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            if (_input.look.x > 0.2) //Right
            {
                _animator.SetBool(_animIDRightLook, true);
            }
            else if (_input.look.x < -0.2) //Left
            {
                _animator.SetBool(_animIDLeftLook, true);
            }
            else { //Not Rotating
                _animator.SetBool(_animIDRightLook, false);
                _animator.SetBool(_animIDLeftLook, false);
            }

            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {   
            float targetSpeed = 0f;

            if(_animator.GetBool(_animIDCrouch)){
                // set target speed based on move speed, sprint speed and if sprint is pressed while crouching
                targetSpeed = _input.sprint ? ((_input.move.y <= -0.9f && _input.move.y >= -1.0f) ? 3f : SprintSpeed*0.7f) : MoveSpeed;
            }
            else {
                // set target speed based on move speed, sprint speed and if sprint is pressed
                targetSpeed = _input.sprint ? ((_input.move.y <= -0.9f && _input.move.y >= -1.0f) ? 3f : SprintSpeed) : MoveSpeed;
            }

            if (_hasAnimator)
            {
                _animator.SetBool(_animIDBack, false);
                _animator.SetBool(_animIDRight, false);
                _animator.SetBool(_animIDLeft, false);
            }

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) {
                targetSpeed = 0.0f;

                //Forward
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving

            if (_input.move != Vector2.zero)
            {
                float rotation;
                if(_input.move.y <= -0.9f && _input.move.y >= -1.0f){ //Back
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDBack, true);
                    }
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;     
                    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation-180f, ref _rotationVelocity, RotationSmoothTime);       
                }
                //else if(_input.move == new Vector2(1.0f, -1.0f)){ //Back-Right
                else if((_input.move.x > 0.1f && _input.move.x < 1.0f) && (_input.move.y > -0.9f && _input.move.y < 0.0f)){ //Back-Right
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDBack, true);
                        _animator.SetBool(_animIDRight, true);
                    }
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;  
                    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation+270f, ref _rotationVelocity, RotationSmoothTime);       
                }
                //else if(_input.move == new Vector2(-1.0f, -1.0f)){ //Back-Left
                else if((_input.move.x > -1.0f && _input.move.x < -0.1f) && (_input.move.y > -0.9f && _input.move.y < 0.0f)){ //Back-Left
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDBack, true);
                        _animator.SetBool(_animIDLeft, true);
                    }
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation-270f, ref _rotationVelocity, RotationSmoothTime);       
                }
                else if (_input.move.y >= 0.9f && _input.move.y <= 1.0f){ //Forward
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                }
                //else if(_input.move == new Vector2(1.0f, 0.0f) || _input.move == new Vector2(1.0f, 1.0f)){ //Right //Forward-Right
                else if((_input.move.x <= 1.0f && _input.move.x > 0.1f) && (_input.move.y >= 0.0f && _input.move.y < 1.0f)){ //Right //Forward-Right
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDRight, true);
                    }
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;     
                    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation-90f, ref _rotationVelocity, RotationSmoothTime);       
                }
                else { //Left //Forward-Left
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDLeft, true);
                    }
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;     
                    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation+90f, ref _rotationVelocity, RotationSmoothTime);        
                }
                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                        _animator.SetBool(_animIDCrouch, false);
                        _input.crouch = false;
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private void Crouch(){
            if(_input.crouch){
                _input.crouch = false;
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDCrouch, !_animator.GetBool(_animIDCrouch));
                    if(_animator.GetBool(_animIDCrouch)){
                        //Reduce Collider
                        _controller.height = 1.2f;
                        _controller.center = new Vector3(0, 0.63f, 0);
                    }
                    else{ 
                        //Increase Collider
                        _controller.height = 1.45f;
                        _controller.center = new Vector3(0, 0.76f, 0);
                    }
                }
            }
        }

        private void UseSword()
        {
            if(_input.sword > 0f){
                _animator.SetBool(_animIDSword, true);
            }
            else {
                _animator.SetBool(_animIDSword, false);
            }
        }


        private void CheckInteraction()
        {
            if(Sword.activeSelf){
                _rayOrigin = _controller.transform.position + new Vector3(0, 0.7f, 0) + _controller.radius/2 * _controller.transform.forward;
                Ray ray = new Ray(_rayOrigin, _controller.transform.forward);
                RaycastHit hit;
                Debug.DrawRay(_rayOrigin, _controller.transform.forward*_interactionDistance, Color.red);

                if (Physics.Raycast(ray, out hit, _interactionDistance)) {
                    if(hit.transform.tag == "Slashable"){
                        Debug.Log("HIT --> " + hit.transform.gameObject);
                        _hitFlag = 1;
                    }
                    else _hitFlag = 0;
                    /*
                    else if(hit.transform.tag == "Interactable"){
                        Debug.Log("Interact --> " + hit.transform.gameObject);
                        _hitFlag = 2;
                    }
                    */
                }
                else _hitFlag = 0;
            }
            else {
                _hitFlag = 0;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        public void teleport(Vector3 pos)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.localPosition = pos;
            gameObject.GetComponent<CharacterController>().enabled = true;
        }

        public void UI_setActive(bool active)
        {
            stats.UI_active = active;
            //gameObject.GetComponent<CharacterController>().enabled = !active;
        }

        private void OnSlash(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                CheckInteraction();
                if (_hitFlag == 0 && SwordSlashAudioClip) //Simple Slash
                    PlaySound(2);
                else if(_hitFlag == 1 && SwordSlashHitAudioClip)//Slashable
                    PlaySound(0);
                /*
                else if(_hitFlag == 2 && SwordSlashAudioClip){//Interactable with Sword
                    Debug.Log("Interactable");
                    PlaySound(1);
                }
                */
                if (SwordEffect){
                    SwordEffect.Play();
                }
            }
        }

        private void WeaponAppear(AnimationEvent animationEvent)
        {
            if (Sword){
                Sword.SetActive(true);
            }
        }

        private void WeaponDisappear(AnimationEvent animationEvent)
        {
            if (Sword)
                Sword.SetActive(false);
        }

        public void PlaySound(int num){
            if(num == 0){ //hit the object
                AudioSource.PlayClipAtPoint(SwordSlashHitAudioClip, transform.TransformPoint(_controller.center), 1);
            }
            if(num == 1){ //interactable PickUp
                AudioSource.PlayClipAtPoint(SwordSlashInteractableAudioClip, transform.TransformPoint(_controller.center), 1);
            }
            if(num == 2){ //use sword
                AudioSource.PlayClipAtPoint(SwordSlashAudioClip, transform.TransformPoint(_controller.center), 1);
            }
            if(num == 3){ //burning sound
                _audioSource.clip = FireAudioClip;
                _audioSource.loop = true;
                _audioSource.Play();
            }
            if(num == 4){ //burning sound
                AudioSource.PlayClipAtPoint(BlubHurtAudioClip, transform.TransformPoint(_controller.center), 3);
            }
            if(num == -1){
                _audioSource.Stop();
            }
        }
        
        public void Damage(string type, bool on_off, DamageVideo bloodEffect){
            if(type == "Lava"){
                if (_hasAnimator){
                    _animator.SetBool(_animIDLavaDamage, on_off); 
                }
            }
            else if (type == "LavaHit"){
                if (_hasAnimator){
                    _animator.SetBool(_animIDLavaDamage, true); 
                }
                if(on_off)
                    GetDamage(bloodEffect);
            }
            else if (type == "Pistone" || type == "Spuntone" || type == "Spine"){
                if(on_off)
                    GetDamage(bloodEffect);
            }
        }

        private void GetDamage(DamageVideo bloodEffect){
            if(!stats.lifeDown(1)){
                //Die
                _gameOver = true;
                bloodEffect.Play();
                PlaySound(4);
                _animator.SetBool(_animIDDeath, true); 
                gameObject.GetComponent<CharacterController>().enabled = false;
                float y = _animator.GetBool(_animIDLavaDamage) ? -0.47f : 0.0f;
                transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            }
            else {
                //Get Damage
                bloodEffect.Play();
                PlaySound(4);
            }
        }

        private void GameOver(AnimationEvent animationEvent){
            //Canvas Appears after animation (On Event)
            stats.UI_GameOver.SetActive(true);
            Cursor.visible = true;
        }


    }
}