# Unity Sockets

This library is an attempt to provide an easy, multiplatform networking library for Unity that leverages both [Valve's GameNetworkingSockets library](https://github.com/ValveSoftware/GameNetworkingSockets) as well as [nxrightthere's C# wrapper](https://github.com/ValveSoftware/GameNetworkingSockets) for that library.

Note: This currently doesn't work.

nxrightthere's lib is included in the repo as a subtree, so upon cloning the repo you will automatically get their code as well. To update their code in your local clone, run this:

```
git fetch csharpsocks master
git subtree pull --prefix Assets/ValveSockets-CSharp/ csharpsocks master --squash
```

Additonally, this currently only contains the compiled GameNetworkingSockets lib for OSX. I'll add a compiled PC binary eventually as well.

## TODO
I'd love for this library to grow into a set of baseline functionality that people can easily leverage for common networking based actions in games. To that end, if people have feature requests for the library, please submit an issue!