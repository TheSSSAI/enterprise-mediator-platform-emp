# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-029 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Creates New Project |
| As A User Story | As a System Administrator, I want to create a new ... |
| User Persona | System Administrator. This user has full permissio... |
| Business Value | This is the foundational step for the entire busin... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Entity Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful Project Creation

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator on the main project dashboard

### 3.1.5 When

I click the 'Create New Project' button, select an active client from the searchable dropdown, enter a unique project name, and click 'Create Project'

### 3.1.6 Then

A new project record is created in the database with a default status of 'Pending', the project is correctly associated with the selected client's ID, I am redirected to the new project's workspace page, and a success notification 'Project [Project Name] created successfully' is displayed.

### 3.1.7 Validation Notes

Verify the new project record in the database, check the foreign key to the client table, and confirm the initial status is 'Pending'. The creation event must be logged in the audit trail.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to Create Project with No Name

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am on the 'Create New Project' form

### 3.2.5 When

I select a client but leave the 'Project Name' field blank and attempt to submit the form

### 3.2.6 Then

The form submission is blocked, an inline validation error message 'Project name is required' is displayed below the name field, and no project is created.

### 3.2.7 Validation Notes

The 'Create Project' button should ideally be disabled until all required fields are filled. Verify no new record is created in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to Create Project with No Client Selected

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the 'Create New Project' form

### 3.3.5 When

I enter a project name but do not select a client and attempt to submit the form

### 3.3.6 Then

The form submission is blocked, an inline validation error message 'A client must be selected' is displayed below the client selection field, and no project is created.

### 3.3.7 Validation Notes

Verify no new record is created in the database.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Client Selection List Excludes Inactive Clients

### 3.4.3 Scenario Type

Business_Rule

### 3.4.4 Given

The system contains both 'Active' and 'Inactive' clients

### 3.4.5 When

I open the client selection dropdown on the 'Create New Project' form

### 3.4.6 Then

The list of clients only contains clients with the status 'Active'.

### 3.4.7 Validation Notes

Requires test data with clients in both states. The API endpoint populating the dropdown must filter by client status.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Cancel Project Creation

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am on the 'Create New Project' form and have entered some data

### 3.5.5 When

I click the 'Cancel' button or close the modal

### 3.5.6 Then

The form is dismissed, no project is created, and I am returned to the project dashboard.

### 3.5.7 Validation Notes

Verify that no data is persisted and the user's context is returned to the state before initiating the creation process.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A primary button on the project dashboard labeled '+ New Project'.
- A modal dialog for the creation form.
- A text input field for 'Project Name' with a clear label.
- A searchable dropdown/combobox for 'Select Client'.
- A primary action button labeled 'Create Project'.
- A secondary action button labeled 'Cancel'.

## 4.2.0 User Interactions

- The 'Create Project' button should be disabled until all required fields (Project Name, Client) are validly populated.
- The client dropdown should support typing to search/filter the list of active clients.
- Validation errors should appear inline as the user interacts with the form or upon attempted submission.

## 4.3.0 Display Requirements

- The client dropdown should display the full company name of the client.
- Upon successful creation, a toast or banner notification should confirm the action.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- The modal must be fully keyboard navigable, including trapping focus within the modal.
- The searchable dropdown must be accessible to screen readers (e.g., using ARIA attributes).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Project must be associated with an existing, active Client.

### 5.1.3 Enforcement Point

During project creation, the client selection list is filtered to only show active clients.

### 5.1.4 Violation Handling

It is impossible to select an inactive client, thus preventing violation.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A new project must be initialized with a 'Pending' status.

### 5.2.3 Enforcement Point

Backend service logic during the creation of the project record.

### 5.2.4 Violation Handling

This is a system-enforced rule; no user action can violate it.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-012

#### 6.1.1.2 Dependency Reason

Requires the ability to create clients, as a project must be associated with a client.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-018

#### 6.1.2.2 Dependency Reason

Requires the ability to deactivate clients to properly test the 'active clients only' business rule (AC-004).

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-006

#### 6.1.3.2 Dependency Reason

Requires user login functionality for the System Administrator to access the platform.

## 6.2.0.0 Technical Dependencies

- A defined database schema for 'Projects' and 'Clients' (REQ-DAT-001).
- A backend API endpoint for creating projects (e.g., POST /api/v1/projects).
- Authentication and RBAC middleware to secure the endpoint.
- An audit logging service to record the creation event.

## 6.3.0.0 Data Dependencies

- The system must contain at least one 'Active' client to allow for project creation.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to fetch the list of active clients for the dropdown must respond with a p95 latency of < 250ms, even with 10,000+ clients in the database. This implies server-side search/pagination.
- The project creation transaction (API call to database commit) should complete in < 250ms.

## 7.2.0.0 Security

- The API endpoint for project creation must be accessible only to users with the 'System Administrator' role.
- All user input (e.g., Project Name) must be sanitized to prevent XSS attacks.

## 7.3.0.0 Usability

- The process of creating a project should be intuitive and require minimal steps.
- Error messages must be clear and guide the user to correct the input.

## 7.4.0.0 Accessibility

- The creation form must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a performant, accessible, and user-friendly searchable dropdown component for client selection that uses server-side filtering.
- Ensuring the project creation process is an atomic transaction.
- Correct integration with the audit logging service to capture the 'before/after' state (in this case, just the 'after' state of the new record).

## 8.3.0.0 Technical Risks

- The client selection component could have poor performance if not implemented with efficient server-side searching, leading to a slow UI.
- Failure to properly handle transaction rollbacks could lead to orphaned data if part of the creation process fails.

## 8.4.0.0 Integration Points

- User Service (for role verification).
- Client Data Store (to fetch active clients).
- Project Data Store (to create the new project).
- Audit Log Service (to record the event).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful project creation and redirection.
- Test all form validation rules (required fields).
- Confirm that only active clients appear in the selection list.
- Test the cancellation flow.
- Verify the API endpoint rejects requests from users without the 'System Administrator' role.
- Check the audit log to ensure the creation event is recorded correctly.

## 9.3.0.0 Test Data Needs

- A test user with the 'System Administrator' role.
- At least two 'Active' clients.
- At least one 'Inactive' client.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage for new logic
- E2E tests for the happy path and key error conditions are passing
- User interface reviewed and approved by the product owner/designer
- Performance of the client search dropdown is verified against requirements
- Security requirements (RBAC) are validated
- Online user documentation for creating a project is drafted or updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the project management workflow and should be prioritized early.
- The implementation of the searchable client dropdown component may be a reusable element and should be built with that in mind.

## 11.4.0.0 Release Impact

This feature is critical for the initial release (MVP) as it enables the core business process.

