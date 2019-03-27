# Unity Sockets

This library is an attempt to provide an easy, multiplatform networking library for Unity that leverages both Valve's GameNetworkingSockets library as well as nxrightthere's C# wrapper for that library.

Currently though, this is an empty Unity project.

nxrightthere's lib is included in the repo as a subtree, so upon cloning the repo you will automatically get their code as well. To update their code in your local clone, run this:

```
git fetch csharpsocks master
git subtree pull --prefix Assets/ValveSockets-CSharp/ csharpsocks master --squash
```