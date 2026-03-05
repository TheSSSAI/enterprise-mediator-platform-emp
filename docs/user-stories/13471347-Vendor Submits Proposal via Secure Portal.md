# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-049 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Submits Proposal via Secure Portal |
| As A User Story | As a Vendor Contact, I want to fill out and submit... |
| User Persona | Vendor Contact: An external user representing a ve... |
| Business Value | Enables the core business function of receiving st... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful Proposal Submission (Happy Path)

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a Vendor Contact has accessed the proposal submission portal using a valid, unique, and non-expired link

### 3.1.5 When

they fill in all mandatory fields (Cost, Timeline, Key Personnel) with valid data, optionally upload a document, and click the 'Submit Proposal' button

### 3.1.6 Then

the system validates the inputs successfully, the 'Submit Proposal' button shows a loading state and becomes disabled, the proposal data is saved to the database associated with the correct project and vendor, the proposal's status is set to 'Submitted', and the user is redirected to a confirmation page displaying a success message.

### 3.1.7 Validation Notes

Verify in the database that a new proposal record is created with the correct data, status, and associations. Check S3 to confirm the uploaded document is stored securely. The API response should be a 201 Created.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to Submit with Missing Mandatory Fields

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

a Vendor Contact is on the proposal submission form

### 3.2.5 When

they attempt to submit the form without filling in a mandatory field, such as 'Cost'

### 3.2.6 Then

the submission is prevented, and a clear, inline error message appears next to the empty mandatory field (e.g., 'This field is required').

### 3.2.7 Validation Notes

Use E2E tests to attempt form submission with each mandatory field left blank individually and verify the correct error message is displayed and no API call is made.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to Submit with Invalid Data Format

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a Vendor Contact is on the proposal submission form

### 3.3.5 When

they enter non-numeric text into the 'Cost' field and click 'Submit Proposal'

### 3.3.6 Then

the submission is prevented, and an inline error message appears stating 'Cost must be a valid number'.

### 3.3.7 Validation Notes

Test with various invalid inputs for numeric and date fields to ensure client-side and server-side validation catch them.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to Upload an Oversized or Invalid File Type

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a Vendor Contact is on the proposal submission form

### 3.4.5 When

they attempt to upload a document that is larger than the system's defined size limit (e.g., 10MB) or has an unsupported file extension (e.g., .exe)

### 3.4.6 Then

the upload is rejected, and a user-friendly error message is displayed near the file input, specifying the reason for failure (e.g., 'File size exceeds the 10MB limit' or 'Invalid file type. Please upload a PDF, DOCX, or TXT.').

### 3.4.7 Validation Notes

Prepare test files of various sizes and types to trigger and verify these specific error messages.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to Access Portal After Proposal Submission

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a Vendor Contact has already successfully submitted a proposal for a project

### 3.5.5 When

they attempt to access the same unique submission link again

### 3.5.6 Then

they are shown a page indicating that their proposal has already been submitted and cannot be edited, possibly showing a read-only summary of their submission.

### 3.5.7 Validation Notes

This enforces the business rule REQ-BUS-001. The system must check the proposal status before rendering the form.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Accessing Portal with an Invalid or Expired Link

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

a user has a link to a proposal portal that is expired, has been revoked, or is malformed

### 3.6.5 When

they attempt to navigate to the URL

### 3.6.6 Then

they are presented with a clear error page stating 'This link is invalid or has expired' and are provided with a support contact email.

### 3.6.7 Validation Notes

Test with expired tokens, invalid tokens, and tokens for projects that are no longer in the 'Proposed' state.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Read-only display area for the sanitized Project Brief
- Standardized form with labeled input fields: Cost (numeric), Timeline (text or date range), Key Personnel (text area)
- File upload component with drag-and-drop support and progress indicator
- Primary 'Submit Proposal' button
- Clear visual indicators for mandatory fields (e.g., asterisk)
- Confirmation page with success message and summary

## 4.2.0 User Interactions

- The 'Submit Proposal' button should show a loading spinner and be disabled upon click to prevent duplicate submissions.
- Form validation errors should appear inline, next to the relevant field, upon attempting to submit.
- The file upload component should allow users to select a file from their device or drag it onto the component.
- The UI must be fully responsive, providing an optimal experience on desktop, tablet, and mobile browsers.

## 4.3.0 Display Requirements

- The sanitized Project Brief must be clearly visible and legible.
- The currency for the 'Cost' field should be explicitly stated (e.g., USD, EUR) based on the project's configuration.
- Constraints for file uploads (max size, allowed types) should be displayed near the upload component.

## 4.4.0 Accessibility Needs

- All form elements must have associated `<label>` tags.
- The page must be fully navigable using a keyboard.
- Focus indicators must be clear and visible.
- Error messages must be programmatically associated with their respective form fields for screen reader users.
- The UI must adhere to WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Vendor can only submit one proposal per project invitation. (Ref: REQ-BUS-001)

### 5.1.3 Enforcement Point

Server-side, before processing the submission request. Also client-side by checking status when the link is accessed.

### 5.1.4 Violation Handling

The API request will be rejected with a 409 Conflict error. The UI will display a message indicating a proposal has already been submitted.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Proposals can only be submitted for projects that are in the 'Proposed' state.

### 5.2.3 Enforcement Point

Server-side, upon validating the access token and before rendering the form or accepting a submission.

### 5.2.4 Violation Handling

The user will be shown an error page indicating the project is no longer accepting proposals.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-042

#### 6.1.1.2 Dependency Reason

This story depends on the Admin's ability to distribute a Project Brief, which generates the unique link the vendor uses.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-044

#### 6.1.2.2 Dependency Reason

The proposal portal must display the sanitized Project Brief, the functionality for which is developed in this story.

## 6.2.0.0 Technical Dependencies

- A defined `Proposal` data model in the database (Ref: REQ-DAT-001).
- A secure token generation and validation service for the unique access links.
- Backend API endpoint (`POST /api/v1/proposals`) to handle form submission, including multipart/form-data for file uploads.
- Integration with AWS S3 for secure storage of uploaded documents.
- Integration with the Notification Service (e.g., AWS SQS/SES) to queue the confirmation email (covered in US-050).

## 6.3.0.0 Data Dependencies

- Requires an existing `Project` in the 'Proposed' state.
- Requires an existing `Vendor` record to associate the proposal with.
- Requires the finalized and sanitized 'Project Brief' text to display.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The proposal portal page must have a Largest Contentful Paint (LCP) of under 2.5 seconds (Ref: REQ-NFR-001).
- The API call for proposal submission should have a 95th percentile response time of less than 500ms, accounting for file upload.

## 7.2.0.0 Security

- The unique access link must use a cryptographically secure, non-sequential, and time-limited token.
- All data must be transmitted over HTTPS (TLS 1.2+).
- Uploaded files must be stored securely in S3 with appropriate access controls and scanned for malware upon upload.
- Server-side validation must be implemented to prevent data tampering or invalid submissions, mirroring all client-side checks.

## 7.3.0.0 Usability

- The form should be simple and intuitive, requiring minimal instruction for a user to complete.
- Feedback (success, error, loading) must be immediate and clear.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (Ref: REQ-INT-001).

## 7.5.0.0 Compatibility

- The portal must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge (Ref: REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the secure, single-use tokenized link mechanism.
- Handling multipart/form-data for file uploads on the backend, including streaming to S3.
- Building a robust, accessible, and responsive frontend form with client-side validation.
- Coordinating state between the frontend UI, backend API, database, and S3.

## 8.3.0.0 Technical Risks

- Security vulnerability in the token validation logic could allow unauthorized access.
- Improper handling of large file uploads could lead to server performance issues or timeouts.
- Inconsistent validation rules between client and server could lead to a poor user experience.

## 8.4.0.0 Integration Points

- Backend API for proposal data persistence.
- AWS S3 for document storage.
- Database (PostgreSQL) for storing proposal records.
- Token service for link generation/validation.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- A vendor successfully submits a complete proposal with a file.
- A vendor attempts to submit an incomplete or invalid form.
- A vendor attempts to use an expired or invalid link.
- A vendor attempts to re-submit a proposal for the same project.
- A vendor uploads a file that is too large or of an invalid type.

## 9.3.0.0 Test Data Needs

- Test vendor accounts.
- Projects in the 'Proposed' state.
- Sample files of various sizes (small, large, oversized) and types (valid, invalid).
- Pre-generated valid, expired, and invalid access tokens.

## 9.4.0.0 Testing Tools

- Jest for unit and integration tests.
- Playwright for E2E tests.
- Axe for automated accessibility checks.
- OWASP ZAP or similar for security scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are written and achieve >80% code coverage.
- Critical path E2E tests are implemented and passing.
- The UI is confirmed to be responsive and meets WCAG 2.1 AA standards.
- Security review of the token mechanism and file upload process is complete.
- API documentation (OpenAPI) for the new endpoint is generated and accurate.
- The feature is deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the entire proposal workflow. It blocks the development of admin-facing proposal management features (US-051, US-052).
- Requires both frontend and backend development effort, which should be coordinated.

## 11.4.0.0 Release Impact

This feature is critical for the core business value proposition and must be included in the initial release to vendors.

