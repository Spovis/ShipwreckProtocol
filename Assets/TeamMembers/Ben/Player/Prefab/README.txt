The prefab is structured as so:
Player
|- PlayerBody
|- DebugText
|- ShootParticle

The parent Player gameobject contains a Rigidbody2D, a CapsuleCollider2D, and three scripts: PlayerInput, PlayerLogic, and PlayerStateMachine.

The PlayerBody contains a SpriteRenderer and an Animator. This is what is used to display the player sprite and run the animation clips.

The DebugText contains a TextMeshPro – Text and a MeshRenderer. This is entirely used to display the active state running with the PlayerStateMachine.

The ShootParticle contains a ParticleSystem and is only used for playing particles when the player attacks (shoots their gun).

The PlayerInput script is a self-contained script that handles all the user’s input. It can be accessed with a reference, or you can access it via the singleton (PlayerInput.Instance). Here you have access to many public properties that can be used to see what the user’s current input is. For example, the line PlayerInput.Instance.IsAttackPressed will return true for the frame that the user presses the attack input.

The PlayerLogic script is used for handling miscellaneous things about the player, such as handling generic physics and also storing data like jump force.

The PlayerStateMachine is what handles the player’s behavioral functionality. There are predefined states that each correspond to logic in the game- Attack, Jump, Fall, Idle, Walk, Tread, and Swim. There is a serialized bool field you can enable or disable in the inspector on this component to enable or disable the viewing of the current state (using the DebugText). Note: this text will not be visible in a build of the game, even if enabled.
