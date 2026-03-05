# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-074 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Manages User Roles and Permissions |
| As A User Story | As a System Administrator, I want to view and chan... |
| User Persona | System Administrator. This user has the highest le... |
| Business Value | Enforces Role-Based Access Control (RBAC), a criti... |
| Functional Area | User Management & Security |
| Story Theme | System Administration & Governance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully changes an internal user's role

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged into the platform and on the user management page

### 3.1.5 When

I select a user with the 'Finance Manager' role, change their role to 'System Administrator' via a dropdown, and save the change

### 3.1.6 Then

A success notification is displayed, the user's role is updated in the UI to 'System Administrator', and an audit log entry is created detailing the change (user, old role, new role, timestamp, performing admin).

### 3.1.7 Validation Notes

Verify the UI update, the database record change, and the creation of a correct audit log entry. Also, verify the updated user can now access System Administrator functions on their next login.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

System prevents the last System Administrator from being demoted

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am the only user with the 'System Administrator' role and I am logged in

### 3.2.5 When

I navigate to my own user profile and attempt to change my role to 'Finance Manager'

### 3.2.6 Then

The system prevents the change and displays a clear error message, such as 'Action denied: Cannot demote the last System Administrator account.'

### 3.2.7 Validation Notes

This requires a backend check before committing the change. The user's role in the database must remain unchanged.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin receives a warning when changing their own role

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am a System Administrator, there are other System Administrators, and I am viewing my own profile

### 3.3.5 When

I attempt to change my own role to 'Finance Manager' and click save

### 3.3.6 Then

A confirmation modal appears with a strong warning, such as 'Warning: You are changing your own role and will lose administrative privileges. Are you sure you want to continue?'

### 3.3.7 Validation Notes

Test that the role change only proceeds if the admin confirms the action in the modal. If they cancel, no change occurs.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

A non-admin user cannot access role management functions

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am logged in as a 'Finance Manager'

### 3.4.5 When

I attempt to access the user role management UI or send a request to the role change API endpoint

### 3.4.6 Then

The UI element for changing roles is not visible or is disabled, and the API endpoint returns a 403 Forbidden error.

### 3.4.7 Validation Notes

Verify both frontend (UI controls not present) and backend (API endpoint secured) enforcement.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Role change is reflected in the user's session

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A 'Finance Manager' user is logged in and their role is changed to 'System Administrator' by an admin

### 3.5.5 When

The 'Finance Manager' user's access token expires and they obtain a new one (or they log out and log back in)

### 3.5.6 Then

Their new session reflects 'System Administrator' permissions, and they can access admin-only features.

### 3.5.7 Validation Notes

This validates the integration with the authentication system. The user's permissions should not update mid-session for their current access token, but must update upon token refresh or re-authentication.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Admin cannot change the role of external contacts

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a System Administrator on the user management page

### 3.6.5 When

I view the details for a 'Client Contact' or 'Vendor Contact'

### 3.6.6 Then

The UI to change their role is not present or is disabled, as their roles are fixed.

### 3.6.7 Validation Notes

This story is scoped to internal users ('System Administrator', 'Finance Manager'). The UI should clearly differentiate between internal and external user types.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A user list view (dependency)
- A user detail/edit page
- A read-only display of the user's current role
- A dropdown menu labeled 'Role' populated with available internal roles ('System Administrator', 'Finance Manager')
- A 'Save Changes' or 'Update Role' button
- A confirmation modal for critical actions (e.g., self-demotion)
- Toast notifications for success and error messages

## 4.2.0 User Interactions

- Admin selects a user from a list to view their details.
- Admin clicks the role dropdown to see available roles.
- Admin selects a new role and clicks 'Save'.
- Admin confirms the action in a modal dialog when required.

## 4.3.0 Display Requirements

- The user's name, email, and current role must be clearly visible.
- The list of available roles in the dropdown must be accurate.

## 4.4.0 Accessibility Needs

- All UI controls (dropdowns, buttons, modals) must be fully keyboard accessible (tabbing, enter/space to activate).
- All controls must have appropriate ARIA labels for screen reader compatibility, per WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-SEC-001

### 5.1.2 Rule Description

Only a System Administrator can modify a user's role.

### 5.1.3 Enforcement Point

Backend API Gateway and Service Level.

### 5.1.4 Violation Handling

API request is rejected with a 403 Forbidden status code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-SEC-002

### 5.2.2 Rule Description

The system must always have at least one active System Administrator account.

### 5.2.3 Enforcement Point

Backend User Service, before processing a role change or user deactivation request.

### 5.2.4 Violation Handling

The transaction is blocked, and an error message is returned to the user.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-AUD-001

### 5.3.2 Rule Description

All user role changes must be recorded in the immutable audit trail.

### 5.3.3 Enforcement Point

Backend User Service, after a successful role change.

### 5.3.4 Violation Handling

If the audit log write fails, the primary transaction should ideally be rolled back to maintain consistency (Saga pattern).

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-001

#### 6.1.1.2 Dependency Reason

Requires the ability to create internal users before their roles can be managed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-006

#### 6.1.2.2 Dependency Reason

Requires a login system for the System Administrator to authenticate.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

Requires the audit trail system to be in place to log the role change event.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-XXX (Implied)

#### 6.1.4.2 Dependency Reason

Requires a basic User Management UI that lists all users, from which an admin can select a user to manage.

## 6.2.0.0 Technical Dependencies

- AWS Cognito for user authentication and identity management.
- Backend User Service with a defined data model for users and roles.
- Backend Audit Service for logging events.
- RBAC enforcement middleware at the API Gateway or service level.

## 6.3.0.0 Data Dependencies

- A predefined list of system roles ('System Administrator', 'Finance Manager') and their associated permissions.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update a user's role should complete with a p95 latency of less than 500ms.

## 7.2.0.0 Security

- The API endpoint for changing roles must be protected and only accessible to authenticated users with the 'System Administrator' role.
- The change must be logged in the audit trail with 'before' and 'after' states, as per REQ-FUN-005.
- The system must prevent a state where no System Administrator exists.

## 7.3.0.0 Usability

- The interface for changing a role should be intuitive and require minimal steps.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend logic to prevent the demotion of the last System Administrator.
- Integration with the authentication system (AWS Cognito) to ensure role changes are reflected in user sessions/tokens.
- Deciding on a strategy for session invalidation or token refresh after a role change to ensure permissions are updated promptly.
- Ensuring the atomicity of the role update and the audit log entry.

## 8.3.0.0 Technical Risks

- Risk of creating a lockout scenario if the 'last admin' check is implemented incorrectly.
- Complexity in propagating role changes to active user sessions without forcing a logout, which could impact user experience.

## 8.4.0.0 Integration Points

- User Service (to update the user record)
- Authentication Service / AWS Cognito (to manage roles/groups)
- Audit Service (to log the change)
- API Gateway (to enforce endpoint security)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify an admin can change another internal user's role.
- Verify an admin cannot change their own role if they are the last admin.
- Verify a non-admin user receives a 403 error when attempting to change a role via API.
- Verify the UI for role management is not visible to non-admin users.
- Verify that after a role change, the user's new permissions are active after they log in again.
- Verify the audit log contains the correct details of the role change.

## 9.3.0.0 Test Data Needs

- Test accounts for each role: System Administrator, Finance Manager.
- A scenario with only one System Administrator account in the database.
- A scenario with multiple System Administrator accounts.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >= 80% code coverage and are passing
- E2E tests for critical paths (role change, last admin block) are implemented and passing
- API endpoint security is manually verified to block unauthorized access
- The change is correctly recorded in the audit trail
- User interface reviewed for usability and adherence to design specifications
- Accessibility (WCAG 2.1 AA) requirements have been met and verified
- Documentation for the user management feature is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is foundational for security and should be prioritized early in the development of admin features.
- Requires clear definition of how user sessions/tokens are handled post-role-change before implementation begins.

## 11.4.0.0 Release Impact

- Enables secure onboarding of different internal user types (e.g., Finance Managers), unblocking financial management features.

