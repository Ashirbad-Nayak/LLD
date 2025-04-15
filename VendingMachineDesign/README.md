# Vending Machine Design

This project focuses on the Low-Level Design (LLD) of a Vending Machine system. The goal is to create a modular, scalable, and maintainable design for a vending machine that dispenses items based on user input and payment.

## Features

- Display available items with prices.
- Accept user input for item selection.
- Handle payments using coins.
- Dispense selected items and return change if applicable.
- Maintain inventory and restock items.
- Handle invalid inputs gracefully.

## Design Patterns

This project leverages several design patterns to ensure a robust and flexible architecture:

- **State Design Pattern**: Used to manage the various states of the vending machine (e.g., IdleState, PaymentState, DispenseState), allowing smooth transitions between states based on user actions.
- **Singleton Design Pattern**: Ensures that certain components, such as the `ItemManager`, have a single instance throughout the application 