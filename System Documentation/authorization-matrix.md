```mermaid
flowchart LR

%% =========================
%% System Roles
%% =========================
SA[Super Admin]
MS[Management Supervisor]

%% =========================
%% Activity Roles
%% =========================
AS[Activity Supervisor]
CO[Coordinator]
VW[Viewer]

%% =========================
%% Actions
%% =========================
A1[Assign Management Supervisor]
A2[Create / Update / Delete Activity]
A3[Publish Activity]
A4[Assign Activity Roles]

S1[View Activity Students]
S2[Mark Student Attendance]
S3[View Student Status]
S4[View Student Certificate]

E1[View Activity Evaluations]

%% =========================
%% System-Level Permissions
%% =========================
SA --> A1
MS --> A2
MS --> A3
MS --> A4

%% =========================
%% Activity-Level Permissions
%% =========================
AS --> S1
AS --> S2
AS --> S3
AS --> S4
AS --> E1

CO --> S1
CO --> S2
CO --> S3
CO --> S4

VW --> S1
VW --> S3
```