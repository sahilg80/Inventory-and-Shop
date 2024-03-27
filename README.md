# Inventory-and-Shop
This game employs the Model-View-Controller (MVC) architecture to separate data, logic, and presentation layers for better code organization and maintenance. Singleton pattern ensures that only one instance of certain classes exists, preventing duplication and ensuring global access where necessary. The Observer design pattern facilitates communication between various parts of the game without direct coupling, enhancing flexibility and scalability.

For data management and configuration, you've utilized Scriptable Objects, which are ideal for storing and managing game data in Unity. Object pooling optimizes memory usage and performance by reusing inactive objects instead of instantiating and destroying them frequently.

In the game, players start with an empty inventory and a set amount of currency to engage in trading activities. They can buy and sell items from the market shop list, with transactions affecting their inventory and currency balance. A maximum inventory weight constraint limits the number of items players can carry, adding a strategic element to inventory management.

Overall, your game provides players with the challenge of managing their inventory, making strategic decisions in trading, and optimizing their resources to maximize profit within the constraints of inventory weight and available funds.
