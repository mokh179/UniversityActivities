
# University Activities System – Architecture Overview

## Overview
This system is built using **Clean Architecture** principles to ensure scalability, maintainability, and high performance.

The architecture clearly separates concerns between:
- Domain
- Application
- Infrastructure
- Presentation (Web/UI)

---

## Core Principles
- Business logic is isolated from frameworks and databases
- High-performance queries using JOINs and projections
- Clear separation between Admin and Student flows
- Explicit Use Cases for every business scenario
- DTO-driven communication with UI
- No Navigation-based data loading

---

## Layer Responsibilities

### Domain Layer
- Contains only entities and core business models
- No EF Core, no DTOs, no infrastructure concerns
- Represents real-world concepts (Activity, StudentActivity, Evaluation)

### Application Layer
- Business logic and system behavior
- Use Cases (Create Activity, Register Student, Submit Evaluation, etc.)
- Interfaces (Repositories, UnitOfWork)
- DTOs and Filters
- Paging & Result models

### Infrastructure Layer
- EF Core DbContext
- Repository implementations
- Identity integration
- Database mappings and queries

### Presentation Layer
- Razor Pages / Controllers
- Authorization attributes
- UI binding to DTOs only

---

## Key Design Decisions
- No Generic Repository
- Use JOIN-based queries instead of navigation properties
- Separate Admin and Student responsibilities
- Aggregated Evaluation results only (privacy-first)
- UnitOfWork for transactional consistency

---

## Status
Architecture is finalized and ready for Infrastructure implementation.
