# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## Known Bugs
- If you are inspecting the values of a script that has one of CinderUtil's custom attribute drawers, like `[Disabled]/[DisabledOnPlay]` when the patent object gets destroyed, you'll get the following Editor only exception: `SerializedObject target has been destroyed`.


## [Unreleased]
### Added
### Changed
### Removed
### Fixed



## [v1.3.4] - 2024-06-01
### Added
 - More `ServiceLocator` methods:
   - `bool TryGet<T>(out service)`: Safelly tries to retrieve the registered service. Returns false if it was unable to find the service.
   - `Deregister<T>(service, @unsafe = false)`: Attempts to deregister the provided service. By default it will throw an exception if the registered service instance was different from the provided servide. If running in unsafe mode, the service will be deregistered regardless.



## [v1.3.3] - 2024-05-06
### Changed
 - Moved `GetRandom()` and `GetRandom(RNG)` from being extension methods on enumerated types, to being generic methods in the `Utils` class.



## [v1.3.2] - 2024-05-06
### Added
 - Added more methods to the `Utils` class.
   - `GenerateSeed()`: Generates a random integer to use as a seed.
 - Added extension methods:
   - CollectionExtensions:
     - `AddAll(items)`: Adds all the elements in the target IEnumerable to the collection.
     - `GetRandom(RNG)`: Alternative to `GetRandom()` that uses the provided `System.Random` instead of `UnityEngine.Random`.
   - EnumExtensions:
     - `GetRandom()`: Return a random enum variant.
     - `GetRandom(RNG)`: Alternative to `GetRandom()` that uses the provided `System.Random` instead of `UnityEngine.Random`.



## [v1.3.1] - 2024-05-04
### Added
 - Added more methods to the `Utils` class.
   - `GetMouseViewportPos2D()`, `GetNormalizedMouseViewportPos2D()`: Returns the mouse's position in the viewport.



## [v1.3.0] - 2024-05-03
### Added
 - Added extension methods:
   - TypeExtensions:
     - `Type.HasAttribute<TAttr>()`: Check via Reflection if a Type has a specific `Attribute`.
     - `Type.IsMonoBehaviour()`: Check via Reflection if a Type extends `MonoBehaviour`.

 - Added a basic `ServiceLocator` utility under the `CinderUtils.Services` namespace:
   - `IService` interface to define new services.
   - `MonoBehaviourService` base class that extends both `MonoBehaviour` and `IService` for ease of use.

   - `Register<T>(service, @unsafe = false)`: Will try to register a service. By default it will throw an exception if the service was already registered. If running in unsafe mode, the service reference will be overriden instead.
   - `Get<T>(forced = false)`: Will try to get the reference to the specified service. By default it will throw an exception if the service was not registered. If running in forced mode, the service will be insantiated if not found instead.

   - `[AutoRegisteredService]` Attribute: Services marked with this Attribute will be initialized on the `ServiceLocator.Initialize()` method. This method is automatically called on the `SubsystemRegistration` runtime phase.
     - `IService`s will be instantiated and the instance will be cached in the case that the class is not also a `MonoBehaviour`.
     - `MonoBehaviourService`s (Or any class that extends both `MonoBehaviour` and `IService`) will have a `GameObject` created for them if there wasn't one already present. If using the `MonoBehaviourService` base class, on Awake it will be marked with `DontDestroyOnLoad()`.

### Removed
 - `AssemblyUtils`: Removed reduntant Assembly cache, as it added unnecesary complexity.
   - `PredefinedAssembly` and `PredefinedAssemblyCache`.



## [v1.2.0] - 2024-05-02
### Added
 - Added a `Utils` static class under the `CinderUtils` namespace for general utilities.
   - `GetMousePos2D()`, `GetMousePos3D()`: Return the the world coordinates of the mouse.
   - `ShootMouseRay3D(out hit)`: Shoot a Raycast from the camera in the direction of the mouse.
   - `SecondsToFormattedMinutes(int secs)`, `SecondsToFormattedHours(int secs)`: Formats seconds to strings in the`MM:ss` and `HH:MM:ss` formats respectively.
 - Added `MY_DEBUG` define for use by the user. Can can be toggled with the `Enable 'MY_DEBUG'`/`Disable 'MY_DEBUG'` entries on the `CinderUtils` menu.
 - Added extension methods:
   - VectorExtensions:
     - For `Vector3` and `Vector3Int`:
       - `Vector3.Offset(offsetVector)`: Offsets the vector's XYZ components by adding another vector to it, and returns it.
       - `Vector3.OffsetX(offset)`: Offsets the vector's X component by some amount, and returns it.
       - `Vector3.OffsetY(offset)`: Offsets the vector's Y component by some amount, and returns it.
       - `Vector3.OffsetZ(offset)`: Offsets the vector's Z component by some amount, and returns it.
     - For `Vector2` and `Vector2Int`:
       - `Vector2.Offset(offsetVector)`: Offsets the vector's XY components by adding another vector to it, and returns it.
       - `Vector2.OffsetX(offset)`: Offsets the vector's X component by some amount, and returns it.
       - `Vector2.OffsetY(offset)`: Offsets the vector's Y component by some amount, and returns it.
   - CollectionExtensions:
     - `ICollection.LoopingGet(int i)`
     - `IDictionary.GetValue(key, defaultValue = default)`



## [v1.1.2] - 2024-04-24
### Added
 - Added extension methods:
   - TypeExtensions:
     - `Type.Is<T>()`: Reflection alternative to `someObj is SomeType`. Usage would be `someObj.GetType().Is<SomeType>()`.



## [v1.1.1] - 2024-04-14
### Added
 - Added `CinderConfigurationMenu` class as general place to add menu entries for the package.
 - Added `CINDER_DEBUG` define that can be toggled with the `Enable Debug Mode`/`Disable Debug Mode` entries on the `CinderUtils` menu.
 - Created `CinderDebug` static class for general use. All the methods will be ignored without the `CINDER_DEBUG` define being set.

### Changed
 - Internal library logging now make use `CinderDebug` instead of `UnityEngine.Debug`, making it dependant on the `CINDER_DEBUG` define.



## [v1.1.0] - 2024-04-10
### Added
 - Created a generic `EventBus` system under the `CinderUtils.Events` namespace:
   - To declare an event type, implement the `IEvent` interface for some type (be it a class or a struct).
   - Making use of the static `EventBus` class, you can `Register<T>(EventBinding<T> binding)` bindings to recieve events, or `Raise<T>(T event)` an event so that it gets propagated across bindings.
   - All events are sent through the `EventBus<T>` corresponding to the type parameter `T` of the `Raise<T>()`, tho you could pass in a an event object of a derived class through the base clases's channel if you cast the object.
   - To recieve events, you must create an `EventBinding<T>`, to which you register handler methods to the `OnEvent` and `OnEventNotify` events, and then `Register()` the binding on the `EventBus`. Make sure to `Deregister()` the binding when the object gets destroyed or disabled.
 - Added examples for the event system under `Samples~/Events`.
 - Created some Reflection utilities under the `CinderUtils.Reflection` namespace:
   - `AssemblyUtils` Used to retrieve Types from the game's Assemblies:
     - `PredefinedAssembly`: Used to be able to index into the `PredefinedAssemblyCache`.
     - `PredefinedAssemblyCache`: Easily accesible cache with the Assemblies of unity's predefined assemblies.
     - `GetSubtypesOf<T>(bool, ICollection<PredefinedAssembly>)`: Used to get all the subtypes of some Type `T`. Used internally to manage the `EventBus` cleanup.

### Changed
 - Moved basic code examples from `Samples~/Basic` to `Samples~/General`.



## [v1.0.2] - 2024-02-07
### Added
 - Added base class for custom property drawers: `CinderPropertyDrawer`.
 - Added extension methods:
   - EnumeratorExtensions:
     - `IEnumerator.ToEnumerable()`: Converts an IEnumerator to an IEnumerable.
   - TransformExtensions:
     - `Transform.Children()`: Returns an `IEnumerable` of all the child Transforms of that transform.
   - GameObjectExtensions:
     - `GameObject.OrNull()`: Returns the object or null if the object is null or deinitializing.
     - `GameObject.Children()`: Returns an `IEnumerable` of all the child GameObjects of that GameObject.

### Changed
 - Improved `[ConditionalField]` attributes to take values as `object`, without converting to `string` internally, this allows to check for `null`.
 - Improved examples for the `ConditionalFieldAttribute`.
 - Made custom property drawers (`[Disabled]`, `[DisabledOnPlay]`, `[ConditionalField]`) extend `CinderPropertyDrawer`.



## [v1.0.1] - 2024-02-06
### Added
 - Added basic code examples under `Samples~/Basic`.
 - Added `UPM Git Extension` to package dependencies.

### Changed
 - Updated `.gitignore` to ignore more IDE cache directories (`.vscode` and `.idea`).
 - Updated keywords on the `package.json`.



## [v1.0.0] - 2024-02-06
### Added
 - Created basic unity package with AssemblyDefinitions for the `Editor` and `Runtime` code.
 - The namespace for the `Runtime` AssemblyDefinition is: `CinderUtils`.
 - The namespace for the `Editor` AssemblyDefinition is: `CinderUtils.Editor`.
 - Added utility functions via extension methods under the `CinderUtils.Extensions` namespace.
   - CollectionExtensions:
     - `ICollection.GetRandom()`
   - EnumeratorExtensions:
     - `IEnumerable.NullOrEmpty()`
     - `IEnumerable.ForEach(Action action)`
     - `IEnumerable.ToDebugString()`
 - Implemented some utility attributes with `CustomPropertyDrawer` to enable more editor customization.
   - `[Disabled]`: Disables the field on the Editor, while still being visible.
   - `[DisabledOnPlay]`: Disables the field on the Editor while the Application is running, or the Editor is changing states.
   - `[ConditionalField]`: Shows the anotated field only if the value of the target field matches one of the passed values, otherwise it will be hidden. This behaviour can be reversed with the `inverse` flag. NOTE: It's an `abstact` class with `internal` constructor, use `[ShowIf]` and `[HideIf]` instead.
   - `[ShowIf]` and `[HideIf]`, which are variants of `[ConditionalField]` that set the `inverse` flag accordingly.









[unreleased]: https://github.com/ash-dvlpr/cinder-utils/compare/v1.3.4...master
[v1.3.4]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.3.4
[v1.3.3]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.3.3
[v1.3.2]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.3.2
[v1.3.1]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.3.1
[v1.3.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.3.0
[v1.2.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.2.0
[v1.1.2]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.1.2
[v1.1.1]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.1.1
[v1.1.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.1.0
[v1.0.2]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.2
[v1.0.1]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.1
[v1.0.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.0