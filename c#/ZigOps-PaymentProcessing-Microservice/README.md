# ZIG AZURE Function Template

## Introduction  

---
This is the template for all  Azure Functions.  It has all the basic project structure that is based on the N-tier Architecture. 

## Getting Started

---

1. Clone the repository from Azure DevOps Repository.
2. Rename solution name to the microservice name  E.g  ZigOps.EmployeeManagement  
3. Rename all the projects in the solutions to have following pattern ZigOps.EmployeeManagement.API  or ZigOps.EmployeeManagement.Service
3. Create a new repository in Azure DevOps to push your solution to.
4. Change the remote repository to a new one you have created in step 4.

## Solution Project Structure 

---

### 1. ZigOps.Function.API

This  project/layer contains all the presentation or api layer code . Functions will be written here for. The Project depends on ZigOps.Function.Service or the service layer.

### 2. ZigOps.Function.Service

This project/layer contains all the logic that coordinates application, processes commands and other activities like calculations or conversations. 


### 3. ZigOps.Function.Data 

This  project/layer contains logic to store and retrieve the data used in the applicaton.


## Branching Strategy
---
The following is the branching policy for all projects  which is the GitFlow strategy. The strategy requires has the following branching system. 

- Master : Contains all stable and deployable code. 
- Development:  Contains all good but potentailly unstable code. All feature branches or work should be branched off from this branch.
- Feature: These branches that created by developers to work on new features.  These should always branch off development or dev


## Branch Policies