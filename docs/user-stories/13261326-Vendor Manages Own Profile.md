# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-028 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Manages Own Profile |
| As A User Story | As a Vendor Contact, I want to access and update m... |
| User Persona | Vendor Contact (as defined in REQ-SEC-001) |
| Business Value | Improves data accuracy for AI matching and financi... |
| Functional Area | Entity Management |
| Story Theme | Vendor Self-Service Portal |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

View Profile Information

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user with the 'Vendor Contact' role

### 3.1.5 When

I navigate to the 'My Company Profile' page

### 3.1.6 Then

I can view my company's current profile information, including Company Name, Address, Payment Details (masked), Areas of Expertise, and Primary Contacts.

### 3.1.7 Validation Notes

Verify that all relevant fields from the Vendor data model (REQ-DAT-001) are displayed. Sensitive data like bank account numbers must be masked (e.g., showing only '...1234').

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully Update Non-Sensitive Profile Information

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am viewing my company profile page

### 3.2.5 When

I enter 'edit mode', update the company address, add a new skill tag like 'Data Science', and click 'Save'

### 3.2.6 Then

The system validates the input, persists the changes to the database, displays a success message 'Profile updated successfully', and the page reflects the updated information.

### 3.2.7 Validation Notes

Check the database to confirm the record for the vendor has been updated. The updated skill tag should be usable for semantic search (REQ-FUN-002).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Securely Update Payment Details

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am editing my company profile

### 3.3.5 When

I attempt to modify the 'Payment Details' field, I am prompted to re-enter my password, I enter the correct password, update the Wise ID, and click 'Save'

### 3.3.6 Then

My identity is confirmed, the new payment information is saved, and the action is recorded in the audit trail (REQ-FUN-005).

### 3.3.7 Validation Notes

Verify an audit log entry is created for this specific change, including the user ID and timestamp. Test the re-authentication flow with the auth service.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to Update Payment Details with Incorrect Password

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am prompted to re-enter my password to update payment details

### 3.4.5 When

I enter an incorrect password and click 'Confirm'

### 3.4.6 Then

The system displays an error message 'Incorrect password. Please try again.' and the payment details are not updated.

### 3.4.7 Validation Notes

Ensure the form remains in edit mode and the sensitive fields are not exposed or saved.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to Save with Invalid Data

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am editing my company profile

### 3.5.5 When

I enter an invalid email format for a contact and click 'Save'

### 3.5.6 Then

The system prevents the save, displays a field-level validation error 'Please enter a valid email address', and the form remains in edit mode.

### 3.5.7 Validation Notes

Test against multiple validation rules from REQ-DAT-002, such as email format, required fields, and phone number format.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Cancel Profile Edits

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am editing my company profile and have made several changes

### 3.6.5 When

I click the 'Cancel' button

### 3.6.6 Then

All changes are discarded, the form reverts to the last saved state, and I am returned to 'view mode'.

### 3.6.7 Validation Notes

Verify that no API call to save data is made and the UI correctly reflects the original data.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Read-Only Fields are Not Editable

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a logged-in Vendor Contact editing my profile

### 3.7.5 When

I view my profile information

### 3.7.6 Then

The 'Company Name' and 'Vetting Status' fields are displayed but are read-only and cannot be edited.

### 3.7.7 Validation Notes

Check that the UI elements for these fields are disabled or presented as static text. Verify that even if a malicious user enables the field via browser tools, the backend API rejects any attempt to change these fields.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'My Company Profile' page.
- Clear 'Edit' and 'Save'/'Cancel' buttons to toggle between view and edit modes.
- Standard input fields for text and contact information.
- A user-friendly multi-select/tagging component for 'Areas of Expertise / Skills'.
- A secure modal/dialog for password re-authentication when editing payment details.
- Success and error notification banners/toasts.

## 4.2.0 User Interactions

- The 'Save' button should be disabled until a change is made to the form.
- Field-level validation messages should appear as the user types or on form submission.
- The skills component should support both selecting from a list and adding new, relevant tags.

## 4.3.0 Display Requirements

- Sensitive data (e.g., bank account numbers) must be masked in view mode, showing only the last 4 digits.
- All required fields must be clearly marked with an asterisk (*) or similar indicator.

## 4.4.0 Accessibility Needs

- The form must be fully navigable using a keyboard.
- All form fields must have associated labels for screen reader compatibility.
- The UI must adhere to WCAG 2.1 Level AA standards, as per REQ-INT-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Vendor Contact can only edit the profile of the company they are associated with.

### 5.1.3 Enforcement Point

Backend API (Service Layer)

### 5.1.4 Violation Handling

The API will return a 403 Forbidden status code if a user attempts to access or modify a profile not linked to their account.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Updating sensitive financial information requires session re-authentication.

### 5.2.3 Enforcement Point

Frontend UI flow and Backend API validation.

### 5.2.4 Violation Handling

Access to edit sensitive fields is blocked until the user's current password is provided and verified.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

User must be able to log in to access their profile page.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-020

#### 6.1.2.2 Dependency Reason

An initial vendor profile must exist in the system, created by an Admin, for the Vendor Contact to be associated with and edit.

## 6.2.0.0 Technical Dependencies

- Authentication Service (AWS Cognito) for session management and re-authentication.
- Backend Vendor Service with GET and PATCH/PUT endpoints for the vendor profile.
- Database schema for the 'vendors' table must be finalized.

## 6.3.0.0 Data Dependencies

- The user record must be correctly associated with a vendor ID in the database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Profile page load (LCP) must be under 2.5 seconds (REQ-NFR-001).
- API response time for saving profile updates must be under 250ms (p95) (REQ-NFR-001).

## 7.2.0.0 Security

- All data must be transmitted over HTTPS (TLS 1.2+).
- The user's authorization to edit the specific vendor profile must be re-verified on the backend for every API request.
- All profile updates, especially changes to payment details, must be logged in the immutable audit trail (REQ-FUN-005).
- Input data must be sanitized on the backend to prevent XSS and other injection attacks.

## 7.3.0.0 Usability

- The process of updating the profile should be intuitive and require minimal user guidance.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The page must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the secure re-authentication flow for payment details.
- Building a robust and user-friendly UI component for managing skills/tags.
- Backend logic to handle partial updates (PATCH) to the vendor profile.
- Ensuring proper audit logging for all changes.

## 8.3.0.0 Technical Risks

- Improper handling of sensitive payment data could lead to a security breach.
- The skills tagging system must be designed carefully to integrate with the vector embedding and semantic search functionality (REQ-FUN-002).

## 8.4.0.0 Integration Points

- AWS Cognito for re-authentication.
- Internal Audit Service to log changes.
- PostgreSQL database with pgvector extension for storing and querying skills.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a vendor can successfully update all editable fields.
- Verify validation rules prevent saving of incorrect data.
- Verify the re-authentication flow for payment details works correctly with both valid and invalid passwords.
- Verify a user cannot edit another vendor's profile by manipulating API requests.
- Verify the page is responsive and accessible on different devices and with screen readers.

## 9.3.0.0 Test Data Needs

- Test accounts for the 'Vendor Contact' role.
- Pre-existing vendor profiles with various data states (e.g., with/without payment info, with/without skills).

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Axe-core for automated accessibility checks.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration test coverage meets the 80% threshold (REQ-NFR-006)
- E2E tests for critical paths are implemented and passing
- User interface reviewed and approved by UX/Product Owner
- Security requirements, especially re-authentication and audit logging, are validated
- Accessibility scan (Axe) passes with zero critical violations
- Online help documentation for vendors is updated to explain this feature
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational feature for vendor engagement and data quality. It should be prioritized early in the development cycle, after basic authentication and vendor creation are complete.

## 11.4.0.0 Release Impact

- Enables the vendor onboarding phase (Phase 2) of the rollout strategy (REQ-TRN-001).

