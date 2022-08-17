# Fortune Wheel in Unity 3D

There are a roulette wheel and its launch button on the screen.

By pressing the start button, a player spins the roulette, and after a while it slows down, stopping at one of its elements.

In the Unity Inspector window, you can set up the following options:

<ul>
<li>Number of wheel sectors</li>
<li>An array of roulette elements with their parameters (image, dropout probability)</li>
<li>Time to full acceleration of the roulette wheel</li>
<li>The maximum speed of rotation</li>
<li>Rotation time at maximum speed (before deceleration)</li>
<li>Time until the roulette wheel stops after determining the element to select</li>
<li>The ability to force the dropout of a specific element</li>
</ul>

The functionality of dropping certain sectors works regardless of the location of the arrow. The arrow can be placed anywhere on the circle.

The text with the sector number is located dynamically in the center of the sector.

## Versions Used
<ul>
<li>Unity 2020.3.7f1</li>
<li>DOTween v 1.2.632</li>
</ul>
<img src="https://user-images.githubusercontent.com/48056797/185156707-1e3b0764-24bf-4f34-a4e2-765d2979a4be.png">
