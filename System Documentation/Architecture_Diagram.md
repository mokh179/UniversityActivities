
```mermaid
graph TD
    UI[Web / Razor Pages]
    UI --> Application

    Application --> Domain
    Application --> Infrastructure

    Infrastructure --> Database[(Database)]
    Infrastructure --> Identity[Microsoft Identity]

    Domain -->|Entities| Application
    Application -->|DTOs| UI
```
