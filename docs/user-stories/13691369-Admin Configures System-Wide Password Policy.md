# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-071 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Configures System-Wide Password Policy |
| As A User Story | As a System Administrator, I want to access a dedi... |
| User Persona | System Administrator. This user has the highest le... |
| Business Value | Enhances the platform's security posture by enforc... |
| Functional Area | System Administration & Security |
| Story Theme | System Configuration and Governance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully views and updates the password policy

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged into the platform

### 3.1.5 When

I navigate to the 'Security Settings > Password Policy' page

### 3.1.6 And

the 'Save Policy' button becomes disabled until another change is made.

### 3.1.7 Then

I see a success notification confirming 'Password policy updated successfully'

### 3.1.8 Validation Notes

Verify the new policy is persisted in the database. Verify that subsequent password changes for any user are validated against this new policy.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Policy enforcement during new user registration

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the password policy requires a minimum of 10 characters and at least one number

### 3.2.5 When

a new user attempts to register using a registration link with the password 'Password'

### 3.2.6 Then

the registration form displays a clear error message stating 'Password must be at least 10 characters long and contain at least one number'

### 3.2.7 And

the registration process is blocked until a compliant password is provided.

### 3.2.8 Validation Notes

This must be tested via an end-to-end test that first sets the policy as an admin and then attempts registration as a new user.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Unauthorized access attempt by a non-admin user

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a user with the 'Finance Manager' role and I am logged in

### 3.3.5 When

I attempt to access the '/admin/settings/password-policy' URL directly

### 3.3.6 Then

I am redirected to my default dashboard

### 3.3.7 And

I see an error message stating 'You do not have permission to access this page.'

### 3.3.8 Validation Notes

Check server-side logs for a 403 Forbidden or similar authorization failure. The check must be enforced on the backend API, not just the frontend router.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin enters invalid data into the configuration form

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a System Administrator on the Password Policy page

### 3.4.5 When

I enter 'eight' into the 'Minimum Length' numeric input field

### 3.4.6 Then

an inline validation error appears below the field stating 'Please enter a valid number between 8 and 64'

### 3.4.7 And

the 'Save Policy' button remains disabled.

### 3.4.8 Validation Notes

Test with non-numeric, negative, and out-of-bounds numbers. The bounds (e.g., 8-64) should be defined as a business rule.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Password policy change is recorded in the audit trail

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

a System Administrator has successfully updated the password policy

### 3.5.5 When

another System Administrator views the system's audit trail

### 3.5.6 Then

a new log entry is present with the action 'Password Policy Updated'

### 3.5.7 And

the log entry details include the timestamp, the ID of the admin who made the change, and a snapshot of the policy settings before and after the change.

### 3.5.8 Validation Notes

Verify the audit log entry in the database or UI. The 'before/after' state should be stored as a JSON object for clarity.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated page under 'System Settings > Security'
- A form with a numeric input for 'Minimum Length'
- Checkboxes for: 'Require uppercase letter (A-Z)', 'Require lowercase letter (a-z)', 'Require number (0-9)', 'Require special character (e.g., !@#$%)'
- A 'Save Policy' button, initially disabled
- Toast notifications for success and error messages

## 4.2.0 User Interactions

- The 'Save Policy' button becomes enabled only when a form value is changed.
- Hovering over a setting label displays a tooltip with a brief explanation of the requirement.
- Attempting to save invalid data triggers inline validation messages.

## 4.3.0 Display Requirements

- The page must always display the currently active password policy settings upon loading.
- Help text should indicate the allowed range for the minimum length field.

## 4.4.0 Accessibility Needs

- All form inputs must have corresponding `<label>` tags.
- The page must be fully navigable and operable using only a keyboard.
- Validation errors must be programmatically associated with their respective form fields for screen reader users (e.g., using `aria-describedby`).
- Adheres to WCAG 2.1 Level AA standards as per REQ-INT-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The minimum password length must be an integer between 8 and 64, inclusive.

### 5.1.3 Enforcement Point

Client-side form validation and server-side API validation.

### 5.1.4 Violation Handling

The system rejects the input and returns a descriptive error message. The policy is not saved.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only users with the 'System Administrator' role can view or modify the password policy.

### 5.2.3 Enforcement Point

API Gateway and backend service middleware.

### 5.2.4 Violation Handling

The API request is rejected with a 403 Forbidden status code. The user is shown an access denied message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Any change to the password policy must be recorded in the immutable audit trail.

### 5.3.3 Enforcement Point

Backend service logic after a successful policy update.

### 5.3.4 Violation Handling

If the audit log write fails, the entire transaction should be rolled back, and an error should be returned to the administrator.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-074

#### 6.1.1.2 Dependency Reason

The Role-Based Access Control (RBAC) system must be in place to restrict access to this feature to System Administrators.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-086

#### 6.1.2.2 Dependency Reason

The audit trail system must be functional to log the changes made to the password policy as required for compliance.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-005

#### 6.1.3.2 Dependency Reason

The user registration and password creation logic must be updated to fetch and enforce the configured policy.

## 6.2.0.0 Technical Dependencies

- AWS Cognito: The backend service will need to interact with the AWS SDK to update the password policy on the Cognito User Pool.
- System Configuration Service: A centralized place to store and retrieve system-wide settings like this policy.
- Audit Logging Service: The service responsible for writing to the audit trail.

## 6.3.0.0 Data Dependencies

- User role definitions must be available to the authorization middleware.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The password policy settings page must load in under 2 seconds (p95).
- The API call to save the policy must respond in under 500ms (p95).

## 7.2.0.0 Security

- The API endpoint for updating the policy must be protected against CSRF attacks.
- All changes must be logged in the audit trail as per REQ-FUN-005.
- The feature must be implemented in a way that supports SOC 2 compliance controls regarding change management and access control.

## 7.3.0.0 Usability

- The interface should be intuitive, requiring no special training for a System Administrator.
- Error messages must be clear, user-friendly, and actionable.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge, as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity lies in the backend integration with AWS Cognito. This requires using the AWS SDK, managing IAM permissions for the service to modify the Cognito User Pool, and mapping our UI settings to the specific Cognito policy parameters.
- Ensuring the update transaction is atomic, including the audit log write.
- Coordinating the enforcement of the policy across different parts of the application (registration, password reset, password change).

## 8.3.0.0 Technical Risks

- Incorrect IAM permissions could block the backend service from updating the Cognito User Pool, leading to save failures.
- Potential for a mismatch between the policy stored in our database and the one active in Cognito if the update API call fails partway through.

## 8.4.0.0 Integration Points

- AWS Cognito API (for setting the policy).
- Internal User Authentication Service (for enforcing the policy during password changes).
- Internal Audit Service (for logging changes).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify an admin can set and save a new policy.
- Verify a non-admin cannot access the page or API endpoint.
- Verify a new user registration is blocked by a non-compliant password.
- Verify an existing user changing their password is blocked by a non-compliant password.
- Verify the audit log correctly records the before and after state of a policy change.
- Verify form validation for out-of-bounds and invalid inputs.

## 9.3.0.0 Test Data Needs

- Test user accounts with 'System Administrator' and 'Finance Manager' roles.
- A set of compliant and non-compliant password strings for various policy configurations.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% code coverage for new code
- Automated E2E test for the primary success and failure scenarios is created and passing
- User interface reviewed for usability and adherence to design standards
- Security review completed to ensure endpoint is secure and audit logging is correct
- Accessibility audit passed against WCAG 2.1 AA
- Administrator documentation updated with instructions on how to configure the password policy
- Story deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for achieving higher levels of security compliance. It should be prioritized early in the development of admin features.
- Requires a developer with experience using the AWS SDK and managing IAM permissions for service-to-service communication.

## 11.4.0.0 Release Impact

This is a foundational security feature. Its absence could be a blocker for production release or for passing security audits.

