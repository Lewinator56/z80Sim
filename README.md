# z80Sim
Z80 CPU simulator for bangor university dissertation project

I'm licensing this under MIT, but it would be nice to be given credit if this is used in other works!

Technically, this can be used to simulate any CPU, I just used the framework it probides to simulate a Z80, the underlying framework provides a way to 'tick' a hardware clock, it is then up to the user to implement instructions.
Most of the code is documented, so making changes or understaning what is going on shouldn't be too hard.

## Prerequisites
- .Net Core 3.1

## Building Yourself
- I'd suggest the easiest way to make sure everything is set up correctly is to just clone the repo in visual studio, this will set up any NuGet packages automaticaly for you.
- Click 'build'


## Usage
- Execution can be controlled by the execution panel in the top left, starting stopping and resetting execution can eb controlled here.
- RAM and register states can be inspected in the main panel, the current program counter position is indicated by an arrow pointing to the memory block in the RAM display.

