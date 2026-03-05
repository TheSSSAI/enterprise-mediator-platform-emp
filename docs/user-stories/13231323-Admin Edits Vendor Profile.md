# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-025 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Edits Vendor Profile |
| As A User Story | As a System Administrator, I want to edit all fiel... |
| User Persona | System Administrator. This user has full CRUD perm... |
| Business Value | Ensures data integrity of the vendor database, whi... |
| Functional Area | Entity Management |
| Story Theme | Vendor Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully edits and saves a vendor's profile

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged into the platform and viewing a specific vendor's detail page

### 3.1.5 When

I click the 'Edit Profile' button, modify the 'Company Name' and add a new 'Skill Tag', and then click 'Save Changes'

### 3.1.6 Then

A success notification is displayed, the edit form closes, the vendor detail page now shows the new company name and skill tag, and an audit log is created for the update.

### 3.1.7 Validation Notes

Verify the UI updates immediately. Check the database for the new values. Query the audit log table/service to confirm the 'before' and 'after' states were recorded correctly.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin attempts to save a profile with invalid data

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a System Administrator editing a vendor's profile

### 3.2.5 When

I enter an invalid email address for a contact (e.g., 'test@domain') and click 'Save Changes'

### 3.2.6 Then

The form remains open, an inline validation error message 'Please enter a valid email address' appears next to the email field, and the profile is not updated.

### 3.2.7 Validation Notes

Test with various invalid inputs for different fields (e.g., text in a numeric field, invalid URL, etc.). Ensure the API returns a 400-level error with a structured validation message.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin cancels the edit operation

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am a System Administrator editing a vendor's profile and have made changes to several fields

### 3.3.5 When

I click the 'Cancel' button

### 3.3.6 Then

A confirmation dialog appears asking 'Are you sure you want to discard your changes?'. Upon confirming, the edit form closes, all my changes are discarded, and the vendor detail page displays the original, unmodified data.

### 3.3.7 Validation Notes

Verify that no API call is made to save the data and the frontend state reverts correctly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Non-admin user attempts to access edit functionality

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am logged in as a Finance Manager (or any role other than System Administrator) and am viewing a vendor's detail page

### 3.4.5 When

I look for the 'Edit Profile' button

### 3.4.6 Then

The 'Edit Profile' button is not visible or is disabled.

### 3.4.7 Validation Notes

Also test by attempting to directly access the edit URL or send an API request; the backend must return a 403 Forbidden status.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Audit trail correctly logs the profile change

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A System Administrator has successfully updated a vendor's address from '123 Old St' to '456 New Ave'

### 3.5.5 When

Another System Administrator views the audit trail for that vendor

### 3.5.6 Then

A new log entry is present containing the timestamp, the updating admin's ID, the action 'Vendor Profile Update', the Vendor ID, and a data snapshot showing '{address: "123 Old St"}' in the 'before' state and '{address: "456 New Ave"}' in the 'after' state.

### 3.5.7 Validation Notes

Ensure sensitive data like payment details are masked or handled appropriately in the audit log's 'after' state snapshot, as per data classification policies.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Admin attempts to save a profile that was modified by another user (concurrent edit)

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a System Administrator and have opened the edit form for Vendor X

### 3.6.5 When

Another admin saves an update to Vendor X, and then I try to save my changes

### 3.6.6 Then

The system displays an error message like 'This profile has been updated by another user. Please refresh the page and re-apply your changes to avoid overwriting their work.' and my save operation is rejected.

### 3.6.7 Validation Notes

This requires an optimistic locking mechanism, such as a version number or an 'updated_at' timestamp, that is checked by the backend on every update.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled 'Edit Profile' button on the vendor detail view.
- An editable form (modal or inline) containing all vendor profile fields.
- Input fields for text, address, email, and payment details.
- A multi-select or tag-based input for 'Areas of Expertise / Skills'.
- 'Save Changes' and 'Cancel' buttons.
- Inline validation error messages.
- A success/error notification toast/banner.

## 4.2.0 User Interactions

- Clicking 'Edit Profile' transitions the view from read-only to an editable form.
- The form should be pre-populated with the vendor's current data.
- Clicking 'Save Changes' submits the form data and closes the form on success.
- Clicking 'Cancel' discards changes and closes the form.

## 4.3.0 Display Requirements

- All editable fields from the Vendor data model (REQ-DAT-001) must be present in the form.
- Validation errors must clearly indicate which field is incorrect and why.

## 4.4.0 Accessibility Needs

- The form must be fully navigable using a keyboard.
- All form inputs must have associated `<label>` tags.
- Error messages must be programmatically linked to their respective inputs using `aria-describedby`.
- The UI must adhere to WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only System Administrators can edit vendor profiles.

### 5.1.3 Enforcement Point

API Gateway (route protection) and Backend Service (request authorization).

### 5.1.4 Violation Handling

API returns a 403 Forbidden error. UI hides or disables the edit functionality for unauthorized roles.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All data inputs must be validated against the data model constraints (e.g., email format, valid currency codes).

### 5.2.3 Enforcement Point

Client-side (for immediate feedback) and Server-side (for security and data integrity).

### 5.2.4 Violation Handling

API returns a 400 Bad Request error with details of validation failures. UI displays inline error messages to the user.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Every modification to a vendor's profile must be recorded in the immutable audit trail.

### 5.3.3 Enforcement Point

Backend Service, triggered after a successful database update.

### 5.3.4 Violation Handling

If the audit log fails to write, the entire transaction should be rolled back to ensure consistency, and a high-priority system error should be logged.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-024

#### 6.1.1.2 Dependency Reason

This story adds the 'Edit' capability to the vendor detail view created in US-024.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-086

#### 6.1.2.2 Dependency Reason

The audit trail system must be in place to log the changes made in this story, as required by AC-005.

## 6.2.0.0 Technical Dependencies

- Vendor Service API with a PATCH/PUT endpoint for updates.
- Authentication/Authorization service (e.g., AWS Cognito) to verify user role.
- Audit Logging Service to record changes.
- Frontend component library (Radix UI) for building the form.

## 6.3.0.0 Data Dependencies

- Requires existing vendor data in the database to edit.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the PATCH/PUT request must be under 250ms (p95) as per REQ-NFR-001.

## 7.2.0.0 Security

- The action must be authorized via the RBAC model (REQ-SEC-001).
- All user input must be sanitized to prevent XSS attacks.
- The update transaction must be logged in the audit trail (REQ-FUN-005).

## 7.3.0.0 Usability

- The editing process should be intuitive, with clear feedback on success or failure.
- Error messages must be user-friendly and actionable.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing robust, multi-field server-side validation.
- Implementing optimistic locking to handle concurrent edits.
- Ensuring the 'before' and 'after' state capture for the audit log is reliable and performant.
- Building an accessible and user-friendly form with complex inputs like a tag manager.

## 8.3.0.0 Technical Risks

- Potential for data inconsistency if the audit log write fails after the primary database commit. This should be handled within a single transaction or using a Saga pattern.
- Performance degradation if the 'before' state lookup for the audit log is inefficient.

## 8.4.0.0 Integration Points

- Frontend UI -> API Gateway -> Vendor Service
- Vendor Service -> PostgreSQL Database
- Vendor Service -> Audit Service (via event bus like SNS/SQS)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful update of each individual field.
- Verify successful update of multiple fields at once.
- Test all validation rules for each field with invalid data.
- Test the cancel functionality with and without changes.
- Test the role-based access control for an admin vs. a non-admin role.
- Test the concurrent edit scenario to ensure optimistic locking works.
- Verify the content and structure of the audit log entry after an update.

## 9.3.0.0 Test Data Needs

- A test user with the 'System Administrator' role.
- A test user with a non-admin role (e.g., 'Finance Manager').
- At least one pre-existing vendor profile in the test database.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration test coverage meets the 80% project standard (REQ-NFR-006).
- E2E tests for happy path, validation errors, and cancellation are implemented and passing.
- Security checks (RBAC) are verified.
- Accessibility audit (automated and manual) has been completed and passed.
- The audit log creation is verified and meets requirements.
- Frontend UI has been reviewed and approved by the design/product owner.
- Story has been deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature for data management. It should be prioritized early in the development of the Entity Management module, right after the 'Create' and 'View' functionalities for vendors are complete.

## 11.4.0.0 Release Impact

- Completes a fundamental CRUD operation for vendor management, enabling admins to maintain data quality.

