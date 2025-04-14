# ATM Design

This project focuses on the Low-Level Design (LLD) of an Automated Teller Machine (ATM) system. The goal is to create a modular, scalable, and maintainable design for an ATM.

## Features

- User authentication using PIN.
- Cash withdrawal with balance validation.
- Balance inquiry.
- Support for multiple denominations.

## Design Patterns

This project leverages several design patterns to ensure a robust and flexible architecture:

- **State Design Pattern**: Used to manage the various states of the ATM (e.g., IdleState, WithdrawState, etc.), allowing the system to transition between states seamlessly based on user actions.
- **Chain of Responsibility Pattern**: Implemented for handling cash withdrawal logic, where multiple handlers (e.g., for different denominations like 2000, 500, 100) process the withdrawal request in a chain, ensuring modular and extensible code.
