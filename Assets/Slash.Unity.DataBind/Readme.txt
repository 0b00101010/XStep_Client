// --------------------------------------------------------------------------------------------------------------------
// Data Bind for Unity
// Copyright (c) Slash Games. All rights reserved.
// --------------------------------------------------------------------------------------------------------------------

Setup
-----

- Import DataBind.unitypackage into your project (you probably already did if you read this file).

- If you are using NGUI, double click on the NGUI.unitypackage in Slash.Unity.DataBind/Addons to extract the NGUI specific scripts.

- If it's the first time you are using Data Bind for Unity:
-- Check the examples in DataBind/Examples
-- Read through the documentation at https://bitbucket.org/coeing/data-bind/wiki

- If you encounter any issues (bugs, missing features,...) please create a new issue at the official issue tracker at https://bitbucket.org/coeing/data-bind/issues

- Any feedback, positive as well as negative, is always appreciated at contact@slashgames.org or at the official Unity forum thread at http://forum.unity3d.com/threads/released-data-bind-for-unity.298471/ Thanks for your help!

Changelog
---------

Version 1.19 (23/10/2020)
***************************

* Implement Add method in Core.Data.Collection to make it XML serializable

* Use protected fields for SerializeField members to remove invalid warnings

* Update deprecated XR methods

* Update to 2019.4.13f1

* Add example to use data of a ScriptableObject in a data provider

* #95 Add setting to specify namespaces from which no context types should be used

Version 1.18 (29/03/2020)
***************************

* Replace deprecated PrefabUtility functions

* Updating to Unity 2018.4.20f1

* #92 Remove possible pending initialization of DataContextNodeConnector, otherwise an exception is thrown if the previous initialization was not yet executed

* #93 Always show component selection picker if a game object was dropped on an component ObjectField

* #91 Fix item index passed to CreateItem of ItemsSetter when item was added to collection

Version 1.17 (27/09/2019)
***************************

* Updating to Unity 2018.1 as minimum version

* Issue #90 Use ToggleIsOnProvider instead of deprecated ToggleIsOnGetter in ActiveSetter example

* Aggregations Example - Use data property instead of plain property in Any filter

Version 1.16 (21/03/2019)
***************************

* Issue #88 Add an ActiveSetter for a collection of game objects

* Issue #89 Add unit tests to test xml serialization

* Show public fields in context holder inspector

Version 1.15 (25/10/2018)
***************************

* Issue #87 Allow all references if no object type is provided for GameObjectComponentSelectionField

* Issue #86 Enum value conversion from int

* Issue #82 Compute initial value of ComparisonCheck in its constructor

* Issue #84 Add SelectableInteractableProvider

* Issue #83 Add example for accessing Nth item of a collection via data binding

* Issue #81 Set DataContextNodeConnector to not initialized when context changed to be able to reinitialize it correctly

* Issue #79, #81 Use single initializer to initialize data context node connectors
This way only one LateUpdate is called instead of one for each connector. Additionally initializations are made that are triggered by parent initializations (e.g. in GameObjectItemsSetter). This prevents one frame delays of the childs.

* Issue #80 Using ToggleIsOnProvider instead of deprecated ToggleIsOnGetter in FunWithBooleans example

Version 1.14 (22/07/2018)
***************************

* Convert special characters in format string of StringFormatter to allow line breaks

* Exclude JetBrains, System and Newtonsoft namespaces for context types

* Issue #40 Add a GroupActiveSetter (base class) and a EnumGroupActiveSetter to activate/deactivate a group of game objects

* Issue #71 Add possibility to add data type hint to provider and use it to filter components when dragging game object on to data binding provider

* Issue #27 Add overload for AddBinding to DataBindingOperator to register a callback when the data binding value changed

* Issue #75 Adjust build scripts, so addon packages are still build correctly

* Issue #75 Move addons into separate folder

* Issue #74 Add setting to make data property backing fields case insensitive

* Issue #78 Don't create new collection in CollectionRangeLookup and time the data collection changes, but do incremental changes instead

* Delay setting of context value in ContextDataUpdater to end of frame if node connector is not initialized yet (i.e. no context set and/or init action not executed)

* Cleanup ContextDataUpdaterTest

* Make LateUpdate method of DataBindingOperator public to execute it from unit tests

* Add unit test for two way binding with ComponentSingleSetter, ComponentSingleGetter and ContextDataUpdater

* Clean up obsolete game objects in Synchronizers and TwoWayBindingExample

* Add example for collection aggregations

* Use CollectionAggregation base class to implement the Any filter

* Issue #76 Remove event handler from underlying data provider and INotifyPropertyChanged parent when last event handler was removed from INodeValueObserver

Version 1.0.13 (26/04/2018)
***************************

* Show foldout for member value which is of an unknown type (Otherwise there is no name of the member in the inspector and its children are rendered immediately)

* Update value of CollectionAnyFilter when an item was added/removed

* Add currency string formatter to create a formatted currency amount from a number and a culture name

* Inform about value change of TextureAspectRatioProvider when texture changed

* Add InputFieldTextSynchronizer and SliderValueSynchronizer to component menu

* Call LateUpdate of data context node connector of ComponentSingleGetter, ContextDataUpdater, UnityEventTrigger and ComponentDataSynchronizer

* Draw foldout for custom types in data contexts + Show foldout for member value which is of an unknown type (Otherwise there is no name of the member in the inspector and its children are rendered immediately)

* Add synchronizer, observer and setter for TextMeshPro input field

* Add CollectionAggregation with sum functions for collections

* Register initial items in CollectionObserver.Init

* Issue #72 Add a GameObjectActiveSwitch which activates one game object when a boolean provider is true, otherwise activates a second game object and vice versa

* Issue #69 Check values of constant objects each update when in editor, so the user can play around with the value in the inspector

* Issue #68 Move NGUI examples to Addons/NGUI folder and include atlas for examples

* Handle replacing an item in a collection by ItemsSetter and clear collection in ItemsSetter when it gets disabled (Otherwise items are created even if items setter is disabled)

* Add AspectRatioFitterAspectRatioSetter, RawImageTextureSetter and TextureAspectRatioProvider

* Updating Count of collection only after saving event handler of Item event (ItemAdded, ItemRemoved,...) (Otherwise the updating of the Count property may cause a listener to register for the item events an thus will be informed about the item event although the initial collection already contained it)

* Only show selection popup for provider when more than one component will work

* Issue #17 Use type hint for GameObjectSingleSetter and only show game object component selection if type hint not available (otherwise the reference is already of the right type)

* Issue #17 Add data type hint which is used to only allow references that match the required type

* Issue #70 Call Enable method of DataBindingOperator in OnEnable, even if Start() method wasn't called yet. The Start() method may only be called one frame later if the game object was created in the Start method of another game object (e.g. the GameObjectItemsSetter)

* Call ValueChanged event once for a ConstantObjectProvider, otherwise not all listeners may be updated correctly

* Call initial ValueChanged event of DataContextNodeConnector in LateUpdate of the holding mono behaviour (yield return new WaitForEndOfFrame and yield return null will both wait until the next frame, not only until the end of the current frame. This causes a 1 frame delay between the context setting and the value updates)

* Update ActiveSetter example to test delayed ValueChanged event in DataContextNodeConnector

* Allow observing read-only properties as their value may change internally

* Cancel initial value change callback if a value change occurs in the mean time

* Clear context of DataContextMediator before it is removed, so the context is correctly unregistered

* Add providers for the (vertical) normalized position of a scroll rect

* Make DataBinding implement IDataProvider

* Add ItemReplaced event to data collection

* Add converter from Vector2 to Vector3

* Add provider for RectTransform width

* StrangeIoC - Overwrite bubbleToContext, so it is possible to have the ContextView on the same game object than the view itself (This is useful when views are stored in separate scenes to be isolated)

* Add CollectionAnyFilter

* Add script to use collection changes as Unity events

* Listen to ItemInserted of observed collection in CollectionObserver

* Implement IDataProvider<T> for Property<T> and make class non-sealed

* Return items with concrete type when iterating over Collection<T> instead of returning object type

* Add setter for the alpha value of a CanvasGroup

* Add converters for values -> color and base color + alpha -> color

* Add index to insert item game objects at in GameObjectItemsSetter

Version 1.0.12 (14/01/2018)
***************************

* Issue #67 Change #ifdef code to if switch, so Unity can do their auto script updates correctly (which doesn't consider #ifdefs)

* Issue #67 Add switch for VR namespaces as the namespace changed in Unity 2017.2 and higher

* Issue #64 Add fix of SmoothCollectionsChangesFormatter to obsolete scripts as well

* Issue #64 Reset delay timer in SmoothCollectionChangesFormatter when bound collection is cleared

* Flag SmoothSlotItemsSetter and SmoothLayoutGroupItemsSetter as obsolete, SmoothCollectionChangesFormatter should be used instead

* Add default behaviour to DataBindingOperator.Deinit with removing all bindings

* Show enumerable values of data providers as an imploded string

* Issue #65 Report warnings for missing backing fields if preprocessor define DATABIND_REPORT_MISSING_BACKING_FIELDS is set

* Report initial value change for EnumGetter

* Issue #66 Make sure ConstantObject calls ValueChanged once when enabled

* Don't update target value if data of a ComponentSingleSetter is not initialized yet

* Don't initialize DataBinding if context of DataContextNodeConnector is not initialized yet

* Handling null value for path in DataContextNodeConnector

* Issue #31 Move add ons into separate folder to exclude them easily

* Set initial value of data node to default value of its type when parent object is null

* Add Synchronizers example to show usage

* Add observers and synchronizers for input field and slider

* Add error when trying to assign a target with a wrong type from a data binding to a component single getter

* Make indexer and GetEnumerator hide implementation of non-generic Collection to return the value with the item type instead of object

Version 1.0.11 (12/11/2017)
***************************

* Call first Enable of a data binding operator in the Start method instead of OnEnable to make sure that all other scripts had time to initialize in their Awake method (Unity may call OnEnable of an object before the Awake method of other objects was called, see https://forum.unity.com/threads/onenable-before-awake.361429/)

* Add small SpriteLoading example that also shows the issue #59 (invalid warning from SpriteLoader)

* Issue #58 Add indexer to Collection class

* Issue #63 Add warning if more than one settings objects are found and none is called like the default one

* Issue #63 Use any DataBindSettings object found in resources when not found at default path

* Issue #63 Made DataBindSettings editable again

* Issue #62 Show items of any IEnumerable in ContextHolderEditor and add buttons to remove/add for ICollection members (which includes Collection class)

* Issue #34 Add error when target binding doesn't receive an object of the expected type 

* Use GameObjectItemsSetter instead of LayoutGroupItemsSetter and flag LayoutGroupItemsSetter as obsolete

* Use virtual method NotifyContextOperatorsAboutContextChange instead of IContextHolder interface

* Only update context data in ContextDataUpdater if active and enabled

* Only use Coroutine for delayed value initialization in DataContextNodeConnector if mono behaviour is enabled

* Issue #60 Only register listener when a valueChangedCallback is set, otherwise an initial value 'null' is returned which changes the context value incorrectly

* Issue #60 Add example that shows a bug when switching the context of a context holder

* Issue #49 Delay data update when context changed till end of frame as there may be multiple data context node connectors that have to update their context first

* Use MonoBehaviour a data binding and data context node connector belong to instead of game object

* Add test case for issue #49

* Issue #45 Make GetValue and SetValue of a context work with struct members

* Issue #56 Make Unity callbacks protected in DataBindingOperator, so there is at least a warning when overriding them instead of the virtual methods (A derived class should use the virtual methods (Init, Deinit, Enable, Disable) instead of the Unity callbacks, so the initialization order remains correct.) 

Version 1.0.10 (14/08/2017)
***************************

* Use OnValueChanged without parameter in ContextDataProvider instead of obsolete version

* Clean up RoundToNearestOperation

* DataBindingOperator only IsInitialized when the Init method is called. Otherwise it may not have added its bindings and return the default value.

* Add example of two way bindings (input field and slider)

* Add utility property to NotifyPropertyChangedDataContext to easily access wrapped data object

* Issue #53 Wrap INotifyPropertyChanged class inside NotifyPropertyChangedDataContext automatically in ContextHolder

* Issue #52 Using NETFX_CORE define instead of UNITY_WSA/UNITY_METRO

* Issue #46 Use InputFieldTextProvider/InputValueProvider and ContextDataUpdater instead of obsolete InputFieldTextGetter/InputValueGetter

* Add unit tests to check if a property in a sub context that implements INotifyPropertyChanged can be accessed and triggers value changes

* Issue #51 Use !field.IsPublic instead of field.IsPrivate. field.IsPrivate flag is always false

* Add TypeInfoUtils to have WSA specifics in one place

* Show any list instead of only Collections for ContextHolder in inspector

* Update value of ContextDataProvider when value inside context changed

* Add MeshFilterMeshSetter

* Change folder name from NotifyPropertyChangedExample to NotifyPropertyChanged to be consistent with former examples

* Rename DataContextNode to DataNode and use Branch and Leaf data nodes to build data tree

* Use INodeValueObserver to unify observation of a node value, either with a DataProvider or INotifyPropertyChanged mechanic

* Rename ContextNode to DataContextNodeConnector

* Wrap INotifyPropertyChanged class when selected as context type and context should be created automatically

* Move out some stuff from ContextHolderEditor

* Draw data class in inspector when NotifyPropertyChangedDataContext is used as a wrapper

* Add option to only trigger ValueChanged of an InputFieldTextProvider when editing ended

* Add non-generic version of CollectionDataBinding

* Move common code into BranchDataContextNode and static methods into DataBindingSettingsProvider and out of DataContextNode. Make as much as possible private for DataContextNode

* Issue #48 Special case for getting paths for MasterPath path dropdown

* Add example for usage of MasterPath

* Issue #26 Show paths for custom serializable classes as well

* Add header to DataContextNode

* Issue #47 Use Equals to check for value equality instead of == operator

* Add number increase/decrease buttons to NotifyPropertyChangedExample

* Specify data context type of context holder in NotifyPropertyChangedExample

* Use concrete class instead of NSubstitute in NotifyPropertyChangedDataContextTest

* Use INotifyPropertyChanged classes as context types and create paths for those types

* Cache data context paths in PathPropertyDrawer

* Move data node handling into DataTree to avoid code duplication

* Rename NotifyPropertyChangedContext to NotifyPropertyChangedDataContext

* Add unit tests for NotifyPropertyChangedContext

* Add example to test NotifyPropertyChanged data contexts

* Add wrapper for data context that use INotifyPropertyChanged instead of data properties

* Add null reference check to EnumGetter to avoid an exception

* Add base class for a data provider that provides a constant value


Version 1.0.9 (26/06/2017)
**************************

* Issue #21 Add field to make ActiveSetter not disabled when the game object it is on is deactivated

* Add unit tests for ActiveSetter

* Use IDataProvider interface in DataBinding instead of concrete class (Makes unit testing much easier)

* Add example for ActiveSetter

* Issue #25 Not throwing warning for missing data property of a member of a non-context class

* Issue #39 Only try loading settings from default path if no DataBindSettings asset was found

* Issue #42 Updating count of collection before triggering event, so in the event handler the correct count is available

* Issue #37 Add example how to use data bind with a Unity UI dropdown element

* Don't get concrete collection, but base class in CollectionDataBinding (The CollectionDataBinding might be used non-generic, so it would fail if trying to get a Collection<object> from the binding)

* Add concrete InvokeCommand methods to Command class to call the methods with parameters via UnityEvents

* Change ContextDataProvider to be generic and don't need a concrete implementation to get a data value from a bound context

* Issue #37 Add converter for the items of a collection

* Add setter for the options of a Unity UI dropdown element

* Add utility classes to observe a collection/collection binding

* Use data property in collection example and add button to replace whole collection

* Issue #33 Add ContextDataUpdater to make ComponentSingleGetter obsolete (Instead of the ComponentSingleGetter a ComponentDataProvider and a ContextDataUpdater can be used which makes usage more flexible (data value from component can be used before being set on the context))

* Issue #33 Cache value of target binding instead of calling GetComponent each time

* Use Assert.Throws instead of ExpectedException in unit tests (Unity 5.6 updated to NUnit 3.0 where this attribute doesn't exist anymore)

* Add provider for getting the mesh of a mesh filter

* Add base class for data providers which can't use an event to determine if their component data value changed (E.g. for transform values like position, local position, rotation and local rotation)

* Add converters from Vector3 to Value, Quaternion to Euler Angles and Euler Angles to Quaternion

* Use SetText method instead of text property to set text of TextMeshPro component (Font size was reset when initially set the text via the property.)


Version 1.0.8 (09/04/2017)
**************************

* Add test to check if value of method node is updated when object changed

* Issue #22 Activate prefab again when deactivating it before instantiation of item game object

* Make Enable, Disable, Init and Deinit methods of DataBindingOperator public so they can be called from unit tests

* Issue #22 Don't disable the template of a GameObjectItemsSetter if it is a prefab and running in the editor

* Add formatter to adjust texture of a material

* Add setter for the material of a skybox

* Add provider to get Input Tracking Position

* Add base class to get a data value from a bound context.

* Add property ItemContexts to GameObjectItemsSetter to get all item contexts in a derived class

* Add base class to invert a data value and added inversion operations for a number and Vector3 as well

* Add Provider for the Skybox of a game object

* Add Setter for the text of a TextMeshPro script

* Add UpdateTargetValue method to ComponentSingleSetter which derived classes have to implement instead of using OnValueChanged which is sealed now - This makes sure that the target value is updated as well if the target binding changed, not only if the data value changed.

* Issue #14 Combining nodes in the popup for choosing a context type if there is only one choice anyway

* Use Velocity for smootheners instead of minimum step - There is no fixed amount of steps as the Update method is called with a different delta time each turn

* Add and use base class for Smootheners

* Issue #9 Add special path (#) to reference context itself

* Add component selection for Provider of data binding

* Issue #2 - Add settings to specify the format of the data providers in a context, so underscores are possible as well - The data property/collection fields should follow the naming conventions of the project they are used in, so there has to be a possibility to define how their names are formatted.

* Add base Formatter, MaterialMainTextureFormatter and MeshRendererMaterialSetter

* Add button in inspector to create a context on a context holder at runtime for debugging

* Issue #15 Add example with notifications that are automatically removed when they were displayed for a specific duration

* Add provider for InputTracking.GetLocalPosition

* Add setters for Transform.localPosition and Transform.localRotation

* Add setter for context of data context mediator

* Connect DataContextView with context holder in Start instead of Awake - Otherwise other behaviours may not be ready for the initialization steps that are involved in registering the context

* Add foldouts to collection/sub-context/data dictionary fields in ContextHolderEditor

* Only set new target binding in OnAfterDeserialize in editor if constantTarget is still set - OnAfterDeserialize is also called when doing changes in inspector which makes it impossible to set the type to Context if there is no path set

* Add base class to provide a component from a game object

* Implement missing methods of IList<T> interface for Data Collection

* Use Transform as target for ItemsSetter - A generic version is not necessary as the target is only used to define the parent of the items, which has to be a Transform

* Add property to get a debug name of a data binding for easier debugging

* GameObjectItemsSetter - Inform derived classes about destroyed item when collection is completely cleared

* Don't show obsolete constantTarget field of ComponentSingleGetter

* Issue #12 #13 Add example with many boolean formatters/setters/getters and some ActiveSetters

* Sync context of DataContextView with context holder correctly - OnRegister of the mediator may be called before Awake. If setting the initial context there it would be erased again in DataContextView.Awake

* Add proxy for command - E.g. to switch between different commands depending on another provider


Version 1.0.7 (10/02/2017)
**************************

* Add event ClearedItems to Collection to get the items that were removed as an observer

* Use binding instead of reference in GameObjectSingleSetter

* Add provider which checks if a camera is pointing at a collider

* Add PrefabInstantiator to instantiate a game object from a provided prefab

* Add check for empty key in GameObjectMapping to not cause NullReferenceException in inspector

* Add data provider for the main camera

* Add data provider for a component's transform and game object

* Allow data binding for target of ComponentSingleGetter

* Add base class for component data providers and add provider for a transform rotation

* Add component selection when referencing a game object in a Data Binding

* Add setter for transform rotation

* Fix wrong check for if target binding is set in ComponentSingleSetter

* Add TransformPositionSetter to use it instead of obsolete PositionSetter

* Use binding for target of ComponentSingleSetter instead of reference e.g. to feed target from a data provider

* Add editing for Vector fields in context holder editor

* Add GameObjectTransformProvider to get the transform component of a specific game object

* Add FindGameObjectWithTagGetter to find a game object with a specific tag

* Show "Invoke" button in inspector next to context methods for debugging

* Add StrangeIoC extension classes

* Add GestureInput extension commands

* Make string data properties editable in ContextHolderEditor by passing the member type to DrawContextData

* Add setter for a material float property

* Add converter for 3 single numbers to a Vector3

* Only show current value in data provider inspector if active and enabled

* Deactivate prefab of GameObjectItemsSetter on Awake, so it isn't visible even if a scene game object is used

* Setters - Added GameObjectItemsSetter as a non-generic class, using MonoBehaviour as type.

* Lookups - Added lookup to find an item from a collection that has a specific value at the specified path.

* Loaders - Added warning if sprite resource can't be found.

* Core - Using common interface for property, collection and other data providers.

* Data Bind - Using context type drawer in context holder editor. Using enum popup when drawing context data for an enum member.

* Data Bind - Added buttons to context inspector to add/remove items from a collection.

* Collection - Added base AddNewItem and Remove method to data collection.

* Path Drawer - Added maximum path depth to avoid infinite loops.

* Examples - Adjusted ContextProperty example to use GameObjectItemSetter.

* Utils - Not preserving world position when adding child from prefab to game object in UnityUtils.

* Switches - Removed obsolete RangeOption.cs

* Context - ValueChanged event for data node is also thrown when collection changes internally (e.g. item added, removed, cleared). This way child nodes are informed about a parent value change.


Version 1.0.6 (01/05/2016)
**************************

* ADDED: Setters - Added setter which sets the items of a layout group one-by-one instead of all-at-once.

* ADDED: Setters - Added setter too smoothly set the fill amount of an image.

* ADDED: Setters - Added setter for the canvas sorting order.

* ADDED: Providers - Added provider for a material.

* ADDED: Commands - Added commands which trigger on specific input events.

* ADDED: Setters - Added SmoothSlotItemsSetter which fills its slots over time instead of immediately.

* ADDED: Operations - Initializing arguments in LogicalBoolOperation to make sure they are not null.

* ADDED: Setters - Added event when slot of SlotItemsSetter was destroyed. Activating item in slot in case it was hidden before.

* CHANGED: Setters - Making prefab inactive before instantiation in GameObjectItemsSetter. Otherwise new game object is already initialized before its context is set.

* ADDED: Setters - Added base class for animator parameter setters. Added setter for animator speed. Only setting animator trigger if animator is initialized.

* ADDED: Smootheners - Added data providers which smooth a float or long data value.

* ADDED: Objects - Added object which holds a plain boolean value.

* CHANGED: Lookups - More robust value getter in DictionaryLookup.

* ADDED: Lookups - Added lookup for an item and a range of items in a collection.

* ADDED: Formatters - Added string formatter which returns the lowered text of its data value.

* ADDED: Formatters - Added SmoothCollectionChangesFormatter which provides its bound collection one-by-one instead of all-at-once.

* ADDED: Formatters - Added formatter which uses a fallback data value if its primary one isn't set.

* ADDED: Formatters - Added formatter which sets its value depending on a boolean data value.

* CHANGED: Checks - Using utility methods in ComparisonCheck and EqualityCheck, so they don't have to do their own error handling.

* CHANGED: Commands - Made Command class non-abstract so it can be added to a game object.

* ADDED: Utils - Added TryConvertValue method to ReflectionUtils.

* ADDED: Editor - Improved context holder inspector to show the set context in more detail.

* CHANGED: Data Binding - More robust GetValue<T> method. Making sure OnValueChanged is called on initialization.

* ADDED: Context - Added special value changed triggers if data value is of type Collection.

* ADDED: Data Dictionary - Added key and value type properties. Implemented Add(KeyValuePair<TKey, TValue> item) method.

* ADDED: Editor - Added custom DataProviderEditor to show current value.

* FIXED: Getters - Using onValueChanged instead of obsolete onValueChange event of Unity input field in InputFieldGetter.

* FIXED: SlotItemsSetter - Only hiding item game objects that have no slot to show them again when free slots are available again.

* CHANGED: Providers - StringFormatter checks for null reference of its arguments before using them.

* CHANGED: Setters - SingleSetter uses DataBindingOperator base class.

* ADDED: Triggers - Added UnityEventTrigger which triggers a Unity event when a data triggers is invoked.

* CHANGED: Commands - UnityEventCommand will forward argument to the command method instead of calling the method without an argument.

* ADDED: Providers - Added NumberSwitch for selecting an option depending on an integer number.

* FIXED: Providers - Updating value when data dictionary in DictionaryLookup changed.

* ADDED: Core - Added DataTrigger which can be used to inform a context about a one shot event.

* ADDED: Data Bind - Using deepest context if max path depth set for path.

* CHANGED: Presentation - Using IContextOperator interface to inform all scripts that have to know it about a context change.

* ADDED: Data Dictionary - Triggering OnCollectionChanged when value changes.

* FIXED: Core - Getting correct item type for enumerable node in DataNode.

* ADDED: Core - ContextHolder stores path to context to allow relative paths even for collections and initializers with multiple path parts (e.g. GameObjectItem(s)Setter or ContextHolderContextSetter).

* CHANGED: Core - Storing parent node instead only parent object.

* ADDED: Collections - Changed property "Count" to be a data property which updates data bindings if it changes.


Version 1.0.5 (03/12/2015)
**************************

* ADDED: Editor - Showing context data in inspector during runtime.

* ADDED: Editor - Added object field in inspector for data binding if type is Reference.

* ADDED: Operations - Added tween operation to change a number value over time.

* ADDED: Objects - Added simple number object, mainly for testing.

* ADDED: Operations - Added module operation to ArithmeticOperation.

* ADDED: Setters - Added setter for the interactable flag of a CanvasGroup.

* ADDED: Commands - Added base commands for UnityEvents with parameters and multiple target callbacks.

* CHANGED: Setters - Moved ImageMaterialSetter from foundation to UI/Unity.

* ADDED: Setters - Added possibility to hide empty slots and to shift items on remove to SlotItemsSetter.

* ADDED: Setters - Added setter to set the context of a specified context holder.

* ADDED: Setters - Added setter to enable/disable a behaviour.

* ADDED: Operations - Added mapping from string to game object.

* ADDED: Getters - Added provider for transform.position.

* CHANGED: Getters - ComponentSingleGetter overrides OnEnable/OnDisable and calls base method of DataProvider.

* ADDED: Formatters - Added StringToUpperFormatter.

* ADDED: Commands - Catching exception when invoking command to log more helpful message.

* ADDED: Setters - Added setter for the sprite of a sprite renderer.

* ADDED: Setters - Added setter for the material of an image.

* ADDED: Setters - Added setter for a trigger parameter in an animator.

* ADDED: Providers - Added lookup for data dictionary.

* ADDED: Core - Added DataDictionary to have a simple data mapping in contexts.

* ADDED: Context - Added context node which points to an item in an enumerable.

* ADDED: Core - Added constructor for Collection to initialize from IEnumerable.

* CHANGED: Core - Data provider doesn't listen to value changes when not active.

* CHANGED: Setters - SingleSetter catches cast exception to provide more helpful log message.

* ADDED: Core - Added DataBindingType "Reference" to reference a Unity object. Catching cast exception when getting value of data binding. IsInitialized value is set before setting value to be set already on callbacks.

* CHANGED: Formatters - Added bindings for symbols of the DurationFormatter, so it is more generic and can be localized.

* ADDED: Setters - Added items setter for fixed number of provided slots.

* ADDED: Commands - Adding default values for missing parameters when invoking command.

* ADDED: Switches - Added NumberRangeSwitch and base class for range switches.

* ADDED: Providers - Added ColorObject to provide a single color value.

* ADDED: Converters - Added Texture2DToSpriteConverter and base DataConverter.

* ADDED: Context Holder Initializer - Added Reset method to automatically search for context holder on same game object on creation/reset.

* ADDED: Providers - Added BooleanSwitch to provide a different data value depending on a boolean value.

* FIXED: Reflection - Using platform-independent implementations of IsEnum and BaseType.

* ADDED: Core - Added ContextChanged event to ContextHolder.


Version 1.0.4 (25/07/2015)
**************************

* ADDED: Examples - New example with a StringToUpperFormatter which converts a text to upper case.

* FIXED: Core - Private fields of base classes for derived types are not reflected, so base classes have to be searched for data property holders as well.

* CHANGED: StringFormatter - Forwards the format string if string.Format fails.

* ADDED: Setters - Added non-abstract GameObjectItemsSetter to use to instantiate game objects for the items of a collection.
 
* ADDED: Core - Added indexer, IndexOf and RemoveAt method to Collection class.
 
* ADDED: Context Path - Added property to ContextPathAttribute to set a custom display name for the path to be used by the PathPropertyDrawer.
 
* ADDED: Unity UI - Added setter and getter for Toggle.isOn field.

* ADDED: Examples - Added example for doing equality checks and selection for an enum.

* ADDED: Getters - Added getter for the values of a specific enum type.

* ADDED: Type Selection - Added drawer for a selection of a specific type for a base type.

* CHANGED: ItemsSetter - Creating items even if value is just an enumeration and no Collection.

* ADDED: EqualityCheck - Converting string to enum value to check for equality.

* FIXED: PathPropertyDrawer - Custom path wasn't shown initially.

* CHANGED: TextAssetLoader - Adjusted context menu naming to match class name.

* CHANGED: ArithmeticOperation - Renamed parameters from ArgumentA/ArgumentB to First/Second for consistent naming.

* CHANGED: InvertBoolOperation - Renamed Argument to Data for consistent naming.

* CHANGED: TimeFormatter - Renamed to DurationFormatter.

* FIXED: EqualityCheck - Converting data value before doing equality check to consider comparison e.g. to string constant.

* FIXED: PathPropertyDrawer - All arguments of StringFormatter switched to custom path if switching one to it. Storing custom path flag for each property path separately. (Issue: https://bitbucket.org/coeing/data-bind/issue/3/custom-path-is-shown-for-all-path).

* CHANGED: Context Node - Correctly creating context path which starts from context that was determined by the path depth.

* CHANGED: Context Node - Caching master path and contexts at the same time. Depth value defines the path depth, not the context holder depth.

* ADDED: Tests - Added test when using multiple context holders with a master path in between.

* ADDED: Tests - Added unit tests for relative data path.

* ADDED: Diagnostics - Added script to initialize a ContextHolder with a specific context type.

* CHANGED: Data Bind - Clearing cached contexts when hierarchy changed. Otherwise a wrong cached context is used when a game object is placed under a new parent game object, e.g. on lists.


Version 1.0.3 (17/03/2015)
**************************

* CHANGED: Foundation - Moved data providers into Foundation/Providers folder and there into sub folders like Loaders and Operations.

* ADDED: Getters - Using path drop down.

* ADDED: Core - Windows 8 and Windows Phone 8 support. Using Unity platform defines and providing Windows RT implementations for GetField, GetMethod and GetProperty utility methods.

* CHANGED: Examples - Changed command from OnSendMessage to SubmitMessage in InputFieldGetter example.


Version 1.0.2 (28/02/2015)
**************************

* ADDED: Core - Added exceptions if trying to set a read-only property or method.

* CHANGED: Core - DataNode sets value of field or property instead of data property, so even raw members of a context are updated correctly.

* CHANGED: Bindings - Renamed GameObjectSetter to GameObjectSingleSetter.

* CHANGED: Core - Made some methods of context holder virtual to allow deriving from the class if necessary.

* FIXED: Data Bind - Returning path to context if no path is set after #X notation.

* CHANGED: Data Bind - Logging error instead of exception if invalid path was specified (on iOS exceptions caused a crash).

* CHANGED: Editor - Adjusted component menu titles for bindings to be more consistent.

* ADDED: Setters - Added binding to create a child game object from a prefab with a specific context.

* ADDED: GraphicColorSetter - Setter for color of a Graphic component (e.g. Text and Image).

* CHANGED: ArithmeticOperation - Checking second argument for zero before doing division to avoid exception.

* ADDED: Unity UI - Added SelectableInteractableSetter to change if a selectable is interactable depending on a boolean data value.

* ADDED: Operations - Added sprite loader which loads a sprite depending on a string value.

* ADDED: Core - Added log error when context is not derived from Context class, but path is set. This may indicate that the user forgot to derive from Context class.

* ADDED: Context Path - Added property drawer for a context path property.

* CHANGED: Context Holder - Only creating context automatically if explicitly stated and no context is already set in Awake.

Version 1.0.1 (15/02/2015)
**************************

* ADDED: Context - Getting value for context node possible from field as well. 

* FIXED: Property - Null reference check before converting value of data property.

* CHANGED: Collection - Collection base class implements IEnumerable, not IEnumerable<object> so the type of the concrete collection is identified correctly in Linq queries.

* CHANGED: Bindings - Only triggering setters in OnEnable if data binding was already initialized with initial data values.

* FIXED: Command - Safe cast to delegate in Command.

* CHANGED: Foundation - Changed BehaviourSingleGetter/Setter to ComponentSingleGetter/Setter.

* CHANGED: Foundation - Created base class for ItemsSetter to use it for other collection setters as well.

* CHANGED: UnityUI - Made GridItemsSetter for UnityUI more generic to work for all LayoutGroups.

* CHANGED: Active Setter - Changed naming in menu from "Active" to "Active Setter".

* ADDED: Command - Reset method is used to set initial target behaviour.

* ADDED: Bindings - Added several checks, formatters and setters: ComparisonCheck, EqualityCheck, ArithmeticOperation, InvertBoolFormatter, LogicalBoolFormatter, PrependSign, RoundToNearestOperation, TextFileContentFormatter, TimeFormatter, AnimatorBooleanSetter.

* ADDED: NGUI - Added several setters/getters for PopupList, Button and Slider.

* ADDED: Unity UI - Added serveral setters/getters for Slider and Image control.

* ADDED: Foundation - ComponentSingleSetter checks if target is null before updating the component.

* ADDED: Core - Throwing exception if invalid path is passed to context in RegisterListener, RemoveListener or SetValue.

* CHANGED: Game Object Items Setter - Only created items are removed on clear, not all children.