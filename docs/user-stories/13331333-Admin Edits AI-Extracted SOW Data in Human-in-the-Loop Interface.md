# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-035 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Edits AI-Extracted SOW Data in Human-in-the-... |
| As A User Story | As a System Administrator, I want to review and ed... |
| User Persona | System Administrator responsible for project setup... |
| Business Value | Ensures the accuracy of the Project Brief, which i... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Processing |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully edits and saves multiple data fields

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin is viewing the SOW review page for a project where AI data extraction is complete

### 3.1.5 When

The Admin modifies the 'Scope Summary', adds a new tag to 'Required Skills', removes an existing tag, and updates a 'Timeline' date

### 3.1.6 And

An audit log entry is created capturing the user, action, and a snapshot of the data before and after the change.

### 3.1.7 Then

The system persists the updated Project Brief data to the database, associating it with the correct project.

### 3.1.8 Validation Notes

Verify database record for the project's brief reflects the changes. Check the audit log table for the corresponding entry. The UI should show the new data upon a page refresh.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin attempts to save with invalid data

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A System Admin is on the SOW review page

### 3.2.5 And

A clear validation error message (e.g., 'Scope Summary cannot be empty') is displayed inline with the corresponding field.

### 3.2.6 When

The Admin deletes all content from the 'Scope Summary' field

### 3.2.7 Then

The system prevents the form from being submitted.

### 3.2.8 Validation Notes

Check that the API call to save the data is not made. Verify the error message appears and is associated with the correct input field.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin attempts to navigate away with unsaved changes

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

A System Admin is on the SOW review page

### 3.3.5 And

They have made at least one modification to the form data that has not been saved

### 3.3.6 When

The Admin attempts to navigate to a different page or close the browser tab

### 3.3.7 Then

The browser displays a native confirmation prompt (e.g., 'You have unsaved changes. Are you sure you want to leave?').

### 3.3.8 Validation Notes

Use browser developer tools to confirm the 'beforeunload' event listener is active after the form is dirty and inactive after a save.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

UI state correctly reflects data changes

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A System Admin is on the SOW review page with no changes made

### 3.4.5 When

The 'Save Changes' button is initially disabled

### 3.4.6 And

When the Admin successfully saves the changes

### 3.4.7 Then

The 'Save Changes' button becomes disabled again until a new change is made.

### 3.4.8 Validation Notes

Manually test the button's 'disabled' attribute state in the DOM before and after making edits and saving.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Network or server error during save operation

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A System Admin has made valid changes on the SOW review page

### 3.5.5 When

They click 'Save Changes' and the backend API returns a 5xx server error

### 3.5.6 Then

An error notification (e.g., 'Failed to save changes. Please try again.') is displayed to the user.

### 3.5.7 And

The user's changes in the form are not lost, allowing them to retry the save operation.

### 3.5.8 Validation Notes

Use browser developer tools to mock a 503 error response from the save endpoint and verify the UI behavior.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A form containing editable fields for all AI-extracted data points (e.g., Scope Summary, Deliverables, Required Skills, Timeline).
- Text inputs and text areas for free-text fields.
- A tag-based, multi-select input for 'Required Skills' with autocomplete functionality.
- Date pickers for timeline fields.
- A 'Save Changes' button.
- A non-editable view of the sanitized SOW text for reference, ideally visible alongside the form.

## 4.2.0 User Interactions

- User can type directly into text fields.
- User can add/remove tags from the skills input.
- User can select dates from a calendar widget.
- The 'Save Changes' button is disabled by default and enables only when the form data has been modified ('dirty' state).

## 4.3.0 Display Requirements

- The form must be pre-populated with the data extracted by the AI service.
- Validation errors must be displayed inline, next to the relevant form field.
- Success and error notifications (toasts/snackbars) must be displayed for save operations.

## 4.4.0 Accessibility Needs

- All form inputs must have corresponding `<label>` tags (WCAG 2.1 AA).
- The entire form must be navigable and operable using only a keyboard.
- Focus indicators must be clearly visible on all interactive elements.
- Validation error messages must be programmatically associated with their respective inputs.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The Project Brief data can only be edited when the project is in a pre-distribution state (e.g., 'Pending', 'Proposed'). Once distributed, the brief should become read-only to maintain a consistent record.

### 5.1.3 Enforcement Point

Backend API and Frontend UI

### 5.1.4 Violation Handling

The API will reject update requests for projects in a locked state with a 403 Forbidden error. The UI will render the form fields as disabled/read-only.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All edits to the Project Brief must be recorded in the immutable audit trail.

### 5.2.3 Enforcement Point

Backend Service Layer

### 5.2.4 Violation Handling

If the audit log write fails, the entire save transaction must be rolled back to ensure data consistency and traceability.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-030

#### 6.1.1.2 Dependency Reason

An SOW document must be uploaded to the system before its data can be extracted and edited.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-034

#### 6.1.2.2 Dependency Reason

The AI extraction process must be complete and the initial data must be viewable before the editing interface can be implemented.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., `PUT /api/v1/projects/{projectId}/brief`) to receive and persist the updated brief data.
- A backend API endpoint (e.g., `GET /api/v1/projects/{projectId}/brief`) to fetch the current brief data.
- Database schema to store the structured Project Brief data.
- Frontend state management solution (e.g., Zustand) to handle form state.

## 6.3.0.0 Data Dependencies

- Requires the processed and structured output from the AI Ingestion Service to be available in the database for the specific project.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The SOW review page, including all extracted data, must load in under 2.5 seconds (LCP).
- The save operation (from button click to success/error feedback) should complete in under 500ms on a stable connection.

## 7.2.0.0 Security

- The API endpoint for updating the brief must be protected and only accessible by users with the 'System Administrator' role.
- All user-submitted data must be sanitized on the backend to prevent Cross-Site Scripting (XSS) vulnerabilities.
- The audit log entry must be written in the same atomic transaction as the data update to prevent discrepancies.

## 7.3.0.0 Usability

- The layout should be intuitive, ideally placing the reference SOW text next to the editable fields to minimize context switching.
- The system must provide clear and immediate feedback for user actions (e.g., saving, errors).

## 7.4.0.0 Accessibility

- Must adhere to Web Content Accessibility Guidelines (WCAG) 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The interface must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge, as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Frontend: Implementing a robust, user-friendly form with complex components like a tag editor and managing its state (dirty checking, validation, submission state) can be complex.
- Backend: The update endpoint must handle validation and ensure the update and audit log creation are atomic.
- State Management: Ensuring unsaved changes are not lost on network errors and prompting the user before navigating away requires careful state management.

## 8.3.0.0 Technical Risks

- The tag/skill input component could be more complex than anticipated, potentially requiring a third-party library or significant custom development.
- Ensuring the 'warn on unsaved changes' feature works reliably across all types of navigation (browser back/forward, link clicks, tab closing) can be tricky.

## 8.4.0.0 Integration Points

- Project Service: The backend service responsible for managing project data.
- Audit Service: The service responsible for writing to the audit trail.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful editing and saving of every field in the form.
- Test all validation rules for required fields or specific formats.
- Simulate a server error on save and confirm the UI handles it gracefully.
- Test the 'warn on unsaved changes' prompt by trying to navigate away via different methods.
- As an unauthorized user role, attempt to access the page and submit data to verify security controls.
- Use a screen reader to navigate and edit the form to validate accessibility.

## 9.3.0.0 Test Data Needs

- A project with a fully processed SOW and populated extracted data.
- A project with a processed SOW where some or all extracted data fields are empty.
- User accounts with 'System Administrator' and non-admin roles.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for end-to-end tests.
- Axe for automated accessibility checks.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are written and achieve >80% code coverage for the new logic.
- End-to-end tests for the happy path and key error conditions are implemented and passing.
- UI has been reviewed for consistency with the design system and usability standards.
- Accessibility (WCAG 2.1 AA) has been verified through automated and manual testing.
- Security checks (e.g., input sanitization, authorization) have been validated.
- The audit trail correctly logs the 'before' and 'after' state of the Project Brief data.
- Relevant documentation (e.g., OpenAPI spec for the new endpoint) has been updated.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical story in the core project setup workflow and a blocker for vendor matching (US-039) and brief distribution (US-042).
- Requires both frontend and backend development effort, which should be coordinated.

## 11.4.0.0 Release Impact

This feature is essential for the initial product release (MVP) as it provides the necessary quality control over the AI-driven workflow.

