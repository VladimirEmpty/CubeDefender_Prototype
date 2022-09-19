# Cube Defender

## Описание 

Данная репозитория содержит прототип Cube Defender, код используется в качестве “примера кода” на позицию Unity-developer. Прототип написан без использования сторонних библиотек, для упрощенного чтения кода проекта. В прототипе реализованы ряд основных приемов и паттернов проектирования.

**Примечание** Код полностью отображает возможности разработчика, которые можно будет увидеть в Тестовом Задание.

## Оглавление

<details>
<summary>Список основных элементов</summary>

-[Bootstrap](#Bootstrap)
-[Builder](#Builder)
-[Factory](#Factory)
-[Pool](#Pool)
-[StateMachine](#StateMachine)
-[GameProcessor](#GameProcessor)
-[Service](#Service)
-[Locator](#Locator)
-[MVC](#MVC)

</details>

## Bootstrap

Элемент представлен объектов класса [Bootstrap](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Bootstrap.cs) с реализации принципа единой точки входа. В данном проекте, выполняет функцию настройки проекта при старте, а также загружает сцену `Game`.

**Примечание:** Если использовать Zenject, то функция стартовой настройки проекта автоматически, переходит на объекты типа `Installer`.

## Builder

Группа объектов предназначенная для ступенчатого создания объектов на игровой сцене так и в core слое проекта. Используется модификация паттерна `Builder` (`Liquid Builder`), для удобства чтения и настройки объекта `Builder`. Как пример, ниже приведу часть объектов `Builder`.

* [GameCoreBuilder](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Builder/Main/GameCoreBuilder.cs) - Данный объект Builder выполняет настройку Core модели проекта, использует настройку [GameCommonSetting](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GameSettings/GameCommonSetting.cs)
* [GameFieldBuilde](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Builder/Main/GameFieldBuilder.cs) - Данный объект Builder создает поле заданного типа, использует поля из настройки [CreateGameFieldSetting](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GameSettings/CreateGameFieldSetting.cs)
* [GameResourcesBuilder](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Builder/Main/GameResourcesBuilder.cs) - Данный объект Builder создает основные игровые пул-объектов, использует настройку [GameResourcesSetting](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GameSettings/GameResourcesSetting.cs)
* и т.д.

## Factory

Элемент представлен основным интерфейсом [IFactory](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Factory/IFactory.cs) и два класса, что реализует данный интерфейс:
* [GameObjectFactory](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Factory/GameObjectFactory.cs) - фабрика по созданию GameObject на игровой сцене.
* [NativeObjectFactory](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Factory/NativeObjectFactory.cs) - фабрика по созданию объектов .Net.

Основная задача создание игровых объектов (см. [Pool](#Pool)) или .Net объектов (см. [StateMachine](#StateMachine)) в процессе работы приложения.

## Pool

Элемент представлен классическим пулом объектов [BaseGameObjectPool](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Pool/Base/BaseGameObjectPool.cs), но с небольшими изменениями[BaseAllocatedGameObjectPool](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Pool/Base/BaseAllocatedGameObjectPool.cs), а именно добавления функции сохранения аллокационных данных создаваемых объектов( ссылки на объект). Данная мера необходима для полной очистки игрового поля при перезагрузки игры. 

**Примечание:** Я понимаю, что хранения информации об объектах создаваемые через пул, не является зоной ответственности пула.

Также можно отметить класс [BasePoolComplex](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Pool/PoolComplex/BasePoolComplex.cs), который включает в себя ряд обычных объектов пула, данный класс предоставляет объект по заданному ключу объекта.

## StateMachine

Данный элемент представлен четырьмя основными участниками:
* Статической точкой доступа к работе с [StateMachine](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/StateMachine/StateMachine.cs)
* Классом переключателем состояний [Machine](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/StateMachine/Machine/Machine.cs)
* Классом состояний, что работаю с объектом целью и необходимыми данными, [State](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/StateMachine/State/BaseState.cs)
* Интерфейсом объекта работающего с StateMachine и состояниями, [IStateMachineOwner](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/StateMachine/IStateMachineOwner.cs)

**Примечание:** Обратите внимание, что происходит активное создание и удаление объектов `State` при работе с `StateMachine`, при этом используется [Factory](#Factory)

## GameProcessor

Данный элемент представляет объект класса [GameProcessor](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GameProcessor.cs), основная задача которого является контроль и переключение общего состояния игры. Является активным пользователем [StateMachine](#StateMachine)

## Service

Группа объектов с интерфейсом [IService](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Service/IService.cs), создаются в `единичном экземпляре` и хранятся в [Locator](#Locator). Архитектурно представляет `Input слой`, для взаимодействия участков кода. 

Как пример приведу несколько классов `Service`:

* [EnemyChipService](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Service/Chip/EnemyChipService.cs) Класс-сервис основная задача контроль работы “вражеских фишек” на игровом поле.
* [PlayerChipService](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Service/Chip/PlayerChipService.cs) Класс-сервис основная задача контроль работы “фишек игрока” на игровом поле.
* [PlayerInputService ](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Service/PlayerInputService.cs) Класс-сервис основная задача обработка взаимодействия игрока
* и т.д.

## Locator

Элемент представлен объектами [Locator](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Locator/Locator.cs), основная задача это хранения и выдача объектов по запросу. 
В прототипе представлен только статичный класс [ServiceLocator](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/Locator/ServiceLocator.cs).

Примечание: Весь элемент имитирует работу `DI контейнера`, т.к. рабочий сервис нужно добавить в `Locator`. 

## MVC

Данный элемент предоставляет функционал для работы с `GUI`. Предоставляет очень `упрощенную версию MVC`, где реализованы основные участники:
* Статический класс для доступа к работе с объектами MVC, [ConectorMVC](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GUI/MVC/ConectorMVC.cs)
* Объекты реализующие интерфейс [IView](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GUI/MVC/View/IView.cs)
* Объекты реализующие интерфейс [IGUIController](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GUI/MVC/Controller/IGUIController.cs), в рамках прототипа реализован базовый класс [BaseController](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GUI/MVC/Controller/BaseController.cs)
* Объекты реализующие интерфейс [IModel](https://github.com/VladimirEmpty/CubeDefender_Prototype/blob/main/Assets/CubeDefender/GUI/MVC/Model/IModel.cs) 

Элемент реализует классический архитектурный подход `MVC`, где:
* `View` - объекты визуальной части, у которых имеются поля по основными визуальными свойствами.
* `Model` - объект логической части, которые получают/модифицируют данные из `“бизнес-логики”`.
* `Controller` - объекты логической части, которые на основе данных из `Model` изменяет визуальную часть объектов `View`.

**Примечание:** `Controller` могут быть обновляемые, где указывается `Тег обновления`. 

# Спасибо за уделенное время!