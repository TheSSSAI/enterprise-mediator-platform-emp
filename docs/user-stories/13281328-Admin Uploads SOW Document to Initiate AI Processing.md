# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-030 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Uploads SOW Document to Initiate AI Processi... |
| As A User Story | As a System Administrator, I want to upload a clie... |
| User Persona | System Administrator: An internal user responsible... |
| Business Value | Initiates the core value-generating workflow of th... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Automation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful SOW Upload (Happy Path)

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin is logged in and is viewing the details page of a project with a status of 'Pending'

### 3.1.5 When

The Admin selects a valid SOW document (.docx or .pdf) that is under the 10MB size limit and confirms the upload

### 3.1.6 Then

The system displays a progress indicator during the upload, shows a success notification upon completion, the uploaded document is securely stored in S3 and associated with the project, the project's SOW status is updated to 'PROCESSING', and an event is published to trigger the asynchronous AI processing service.

### 3.1.7 Validation Notes

Verify via UI feedback. Check the project's status in the database. Confirm the file exists in the designated S3 bucket. Check the message queue for the 'SOW_UPLOADED' event.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to Upload Invalid File Type

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A System Admin is on the project details page with the file upload interface visible

### 3.2.5 When

The Admin attempts to select or drop a file with an unsupported extension (e.g., .png, .txt, .zip)

### 3.2.6 Then

The system must prevent the upload and display a clear, user-friendly error message: 'Invalid file type. Please upload a .docx or .pdf file.'

### 3.2.7 Validation Notes

Test with various invalid file types. The error message should appear on the client-side before the upload is initiated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to Upload File Exceeding Size Limit

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A System Admin is on the project details page with the file upload interface visible

### 3.3.5 When

The Admin attempts to upload a valid file type (.docx or .pdf) that is larger than the 10MB limit

### 3.3.6 Then

The system must prevent the upload and display a clear error message: 'File size exceeds the 10MB limit.'

### 3.3.7 Validation Notes

Create a test file larger than 10MB. The validation should ideally happen client-side first, with a mandatory server-side check.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Upload Fails Due to Network Error

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A System Admin has initiated a file upload

### 3.4.5 When

The network connection is lost during the upload process

### 3.4.6 Then

The system must cancel the upload, display an informative error message like 'Upload failed. Please check your connection and try again.', and allow the user to retry the upload without reloading the page.

### 3.4.7 Validation Notes

Simulate network failure using browser developer tools. Verify the UI handles the error gracefully.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Upload Interface Visibility

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

A System Admin is viewing the details page of a project

### 3.5.5 When

The project status is 'Pending' and no SOW has been uploaded yet

### 3.5.6 Then

The SOW upload component must be visible and enabled.

### 3.5.7 Validation Notes

Check that the component is present on a newly created project.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Upload Interface Hidden After Processing Starts

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

A System Admin is viewing the details page of a project

### 3.6.5 When

An SOW has already been successfully uploaded and the SOW status is 'PROCESSING' or 'PROCESSED'

### 3.6.6 Then

The primary upload component should be replaced with information about the current SOW, its status, and potentially an option to 'Replace SOW' (which would be a separate user story).

### 3.6.7 Validation Notes

Verify that after a successful upload, the UI state changes and does not allow a second upload via the same initial component.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section on the project details page for the SOW.
- A file input button and/or a drag-and-drop area.
- A progress bar or spinner to indicate upload activity.
- Toast notifications for success and error messages.
- A text element to display the current SOW status (e.g., 'No SOW Uploaded', 'PROCESSING').

## 4.2.0 User Interactions

- User can click a button to open a native file selection dialog.
- User can drag a file from their desktop and drop it onto the designated area.
- The interface provides immediate visual feedback upon file selection/drop (e.g., showing the filename).
- The user can cancel an upload in progress.

## 4.3.0 Display Requirements

- Clearly state the accepted file formats (.docx, .pdf) and the maximum file size (10MB) near the upload component.
- Error messages must be specific and actionable.

## 4.4.0 Accessibility Needs

- The upload component must be fully keyboard accessible (navigable with Tab, activatable with Enter/Space).
- The component must have appropriate ARIA attributes for screen reader compatibility (e.g., `aria-label`, `role='button'`).
- Error messages must be associated with the input field using `aria-describedby`.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-SOW-001

### 5.1.2 Rule Description

An SOW can only be uploaded to a project in a pre-active state (e.g., 'Pending').

### 5.1.3 Enforcement Point

API Endpoint (Server-side)

### 5.1.4 Violation Handling

The API will reject the request with a 403 Forbidden or 400 Bad Request error, and the UI will display a message like 'SOW cannot be uploaded to a project that is already active.'

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-SOW-002

### 5.2.2 Rule Description

Only .docx and .pdf file types are permitted for SOW uploads.

### 5.2.3 Enforcement Point

Client-side validation and mandatory Server-side validation.

### 5.2.4 Violation Handling

Request is rejected with a 400 Bad Request error and a descriptive error message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-SOW-003

### 5.3.2 Rule Description

The maximum file size for an SOW is 10MB.

### 5.3.3 Enforcement Point

Client-side validation and mandatory Server-side validation.

### 5.3.4 Violation Handling

Request is rejected with a 413 Payload Too Large error and a descriptive error message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

User must be authenticated as a System Admin to access project pages.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-029

#### 6.1.2.2 Dependency Reason

A project entity must exist in the system to associate the uploaded SOW with.

## 6.2.0.0 Technical Dependencies

- Configured AWS S3 bucket for storing original SOW documents with appropriate security policies.
- Backend API endpoint capable of handling multipart/form-data requests.
- Asynchronous event bus (AWS SNS/SQS) must be set up to receive the 'SOW_UPLOADED' event.
- The 'AI Ingestion Service' must be configured to subscribe to the 'SOW_UPLOADED' event topic/queue.

## 6.3.0.0 Data Dependencies

- Requires an existing 'Project' record in the database with a unique identifier.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI must remain responsive during the file upload process.
- The API endpoint for the upload should acknowledge the request and start the transfer within 500ms.

## 7.2.0.0 Security

- All file transfers must occur over HTTPS (TLS 1.2+).
- The S3 bucket for original SOWs must be private and encrypted at rest (AWS KMS).
- Server-side validation of file type, size, and content-type header is mandatory to prevent security vulnerabilities.
- Uploaded files should be scanned for malware before being passed to the AI processing service.

## 7.3.0.0 Usability

- The upload process should be intuitive, requiring minimal steps.
- Feedback to the user (progress, success, failure) must be immediate and clear.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-INT-001.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a robust, secure file upload endpoint on the backend.
- Creating a user-friendly and accessible frontend component with drag-and-drop, progress, and validation.
- Ensuring correct integration with S3 and the asynchronous event bus (SNS/SQS).
- Handling various error states gracefully (network issues, validation failures).

## 8.3.0.0 Technical Risks

- Misconfiguration of S3 bucket permissions could lead to data exposure.
- Failure to properly handle asynchronous event publishing could cause the AI workflow not to trigger, creating a silent failure.
- Inadequate server-side validation could open security holes.

## 8.4.0.0 Integration Points

- Frontend -> Backend API (for file upload)
- Backend API -> AWS S3 (for file storage)
- Backend API -> Database (to update project status)
- Backend API -> AWS SNS/SQS (to trigger AI service)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful upload of a .docx file.
- Verify successful upload of a .pdf file.
- Verify rejection of a .jpg file.
- Verify rejection of a file larger than 10MB.
- Simulate a network failure during upload and verify error handling.
- Test keyboard-only navigation and activation of the upload component.
- Verify the 'SOW_UPLOADED' event is correctly formatted and published to the message queue.

## 9.3.0.0 Test Data Needs

- Sample .docx and .pdf files under 10MB.
- A sample file over 10MB.
- Sample files with invalid extensions (.txt, .png, .exe).

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% coverage and all passing
- E2E tests for the happy path and key error conditions are implemented and passing
- User interface reviewed for usability and adherence to design specifications
- Security checks for file upload vulnerabilities have been performed
- Accessibility of the upload component has been verified (WCAG 2.1 AA)
- All related documentation (e.g., API spec) has been updated
- The story has been deployed and successfully verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the entire SOW processing feature. It is a blocker for subsequent stories like US-031, US-032, and US-034.
- Requires both frontend and backend development effort.

## 11.4.0.0 Release Impact

This feature is critical for the initial release (MVP) as it represents the primary entry point for the core business workflow.

