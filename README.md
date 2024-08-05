# NIGHTMARE
<img src="https://i.imgur.com/CMFZRcX.gif" width="45%"></img>
<img src="https://i.imgur.com/W2dPql3.gif" width="45%"></img>
<br/> <br/>
Nightmare is a platformer prototype which intends to mimic the feeling of flying in a dream. You control a physics-based acrobatic character who has the ability to levitate, but only for a limited amount of time. The player must combine carefully timed jumps with this limited levitation to be able to move around the buildings of the level without colliding or losing speed.

Animations are from Clazy's *Runner Action Pack* and Kubold's *Movement Animset Pro*.

## Gameplay
By using the *left stick* (on gamepad) or the *WASD* keys, the player can freely **run** at different speeds. Depending on how far the input is being pressed.
<br/> <br/>
<img src="https://i.imgur.com/JoSVtSq.gif" width="45%"></img>
<br/> <br/>
By pressing the *south button* (on gamepad) or the *spacebar*, the player can **jump**. Depending on how long the button is pressed, the player jumps higher or lower. The jumping animation also changes depending on the direction the player is moving towards.
<br/> <br/>
<img src="https://i.imgur.com/kTStofY.gif" width="45%"></img>
<img src="https://i.imgur.com/a85BV0f.gif" width="45%"></img>
<br/> <br/>
While in the air, the player can hold the jump button again to **levitate**. Levitation cancels gravity but it uses up **ego**, an energy meter represented by the blue wheel at the center of the screen. When it runs out, the player can't levitate anymore, and must wait for the ego wheel to regenerate to a minimum before levitating again (indicated by the orange hue).
<br/> <br/>
<img src="https://i.imgur.com/UR6iQrr.gif" width="45%"></img>
<img src="https://i.imgur.com/03p6JC9.gif" width="45%"></img>
<br/> <br/>
The player can rotate and change direction mid-air (regardless of whether they're falling or levitating), but they can't build up speed. The only way to build up speed while in the air is through the **thrust** action. By holding the *right trigger* (on gamepad) or the mouse's *left click*, the player is launched forward in the direction of the camera. This action uses up a lot of **ego** though, so it has to be used sparingly.
<br/> <br/>
<img src="https://i.imgur.com/5ysXMBz.gif" width="45%"></img>
<br/> <br/>
All of these actions together shape Nightmare's character into the agile but skill-demanding avatar any good platformer about flying should have. After all, *falling sells the feeling of flight*.

## Controller
There are four main components which form the player controller: the state machine, the physics handler, the animation handler and the ego meter.

### State Machine
Following the mathematical model of computation known as *finite-state machines*, the player is implemented as an abstract machine that can only be in one of four states at a given time (walking, falling, levitating and thrusting). This allows us to encapsulate context-specific behavior and actions into each state without affecting the other states. 

For example, the jumping action is only evaluated in the walking state, which makes it intrisincally impossible to jump while falling or levitating. Also, cancelling gravity for levitation is as simple as disabling gravity for the levitating state (though it's also disabled in thrusting state).
<br/> <br/>
<img src="https://i.imgur.com/pSxNfbY.gif" width="45%"></img>
<img src="https://i.imgur.com/RYr05qX.gif" width="45%"></img>
<br/> <br/>
Each state handles the physics that are relevant to their context, they define the transitions to the other states under the right conditions and enable the actions that can be performed in said context.
