# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-037 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Original SOW |
| As A User Story | As a System Administrator, I want to securely acce... |
| User Persona | System Administrator. This is a trusted internal u... |
| Business Value | Enables legal compliance, contractual verification... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Processing |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful access and download of the original SOW

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator and I am on the Project Workspace page for a project that has a successfully uploaded SOW

### 3.1.5 When

I click the 'View Original SOW' button or link

### 3.1.6 Then

the system initiates a download of the original, un-sanitized SOW file to my local machine using a secure, time-limited URL

### 3.1.7 And

an entry is created in the audit log containing my user ID, the action 'ORIGINAL_SOW_ACCESSED', the project ID, my IP address, and a timestamp.

### 3.1.8 Validation Notes

Verify the downloaded file is the exact, unaltered original. Check the audit log table in the database for the corresponding entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Unauthorized user attempts to access the original SOW

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a logged-in user with a role other than System Administrator (e.g., Finance Manager)

### 3.2.5 When

I attempt to access the original SOW, either because the UI element was mistakenly rendered or by navigating to a direct API endpoint URL

### 3.2.6 Then

the system must prevent the download and I should see a '403 Forbidden' or 'Access Denied' error message.

### 3.2.7 Validation Notes

Test this with a user account having the 'Finance Manager' role. The API call should return a 403 status code.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Original SOW file is missing from storage

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am a logged-in System Administrator on a Project Workspace page, but the associated original SOW file is missing from the S3 bucket

### 3.3.5 When

I click the 'View Original SOW' button

### 3.3.6 Then

the system should display a user-friendly error message, such as 'The original SOW document could not be found. Please contact support.'

### 3.3.7 And

a high-severity error is logged in the application logs (e.g., CloudWatch) with the project ID and the expected file path for investigation.

### 3.3.8 Validation Notes

Simulate this by manually deleting an SOW file from the S3 bucket in a test environment and then attempting to access it via the UI.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

UI state for a project with no SOW uploaded

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am a logged-in System Administrator viewing the details of a project for which no SOW has been uploaded yet

### 3.4.5 When

I view the document management section of the project page

### 3.4.6 Then

the 'View Original SOW' button or link must be either disabled or not visible.

### 3.4.7 Validation Notes

Create a new project without uploading an SOW and navigate to its details page to verify the UI state.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled button or link, e.g., 'Download Original SOW', on the Project Workspace page.
- A lock icon next to the button/link to visually indicate the sensitive and restricted nature of the document.

## 4.2.0 User Interactions

- Clicking the button/link should immediately trigger a file download in the browser.
- Hovering over the button should display a tooltip, e.g., 'Access the original, un-sanitized document. This action will be audited.'

## 4.3.0 Display Requirements

- The button/link should only be enabled and visible if an original SOW document exists for the project.

## 4.4.0 Accessibility Needs

- The button/link must be keyboard-focusable and operable.
- It must have an appropriate ARIA label, such as 'Download original Statement of Work document'.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Access to original, un-sanitized SOW documents is restricted to users with the 'System Administrator' role.

### 5.1.3 Enforcement Point

API Gateway and Backend Service Layer (API endpoint for generating the download link).

### 5.1.4 Violation Handling

The request is rejected with a 403 Forbidden status code. The failed access attempt should not be logged in the primary audit trail, but can be logged as a security warning in system logs.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All successful access events for original SOW documents must be recorded in the immutable audit trail.

### 5.2.3 Enforcement Point

Backend Service Layer, after permission checks pass but before the download URL is returned.

### 5.2.4 Violation Handling

If the audit log entry cannot be created, the request should fail and return a 500 Internal Server Error to prevent unaudited access.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-030

#### 6.1.1.2 Dependency Reason

This story requires the functionality to upload an SOW document to be implemented first, as it needs a file to access.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-086

#### 6.1.2.2 Dependency Reason

This story depends on the existence of the audit trail system to log the access event as required by the acceptance criteria.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-074

#### 6.1.3.2 Dependency Reason

The Role-Based Access Control (RBAC) system for managing user roles must be in place to enforce the access restrictions.

## 6.2.0.0 Technical Dependencies

- AWS S3 for file storage.
- AWS Cognito (or equivalent) for user authentication and role verification.
- A backend API endpoint to handle the request.
- A frontend component within the Project Workspace view.

## 6.3.0.0 Data Dependencies

- Requires a Project entity in the database with a reference to the S3 key of the original SOW document.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response to generate the secure download link should be < 250ms (p95).
- The file download should initiate in the user's browser within 2 seconds of clicking the link.

## 7.2.0.0 Security

- Access must be strictly enforced by the RBAC model (System Admin only).
- The file must be served via a time-limited, pre-signed S3 URL with a short expiry (e.g., 5 minutes or less).
- The S3 bucket containing original SOWs must be private and not publicly accessible.
- The access event must be logged in the audit trail as per REQ-FUN-005.

## 7.3.0.0 Usability

- The control to access the document must be easy to find within the project's context.
- Error messages for failed access or missing files must be clear and user-friendly.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The download functionality must work on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires integration between the backend service, the authentication service (Cognito), S3, and the audit logging service.
- Security is paramount; implementation of pre-signed URLs and robust permission checks must be correct.
- Frontend state management is needed to show/hide the download button based on SOW existence.

## 8.3.0.0 Technical Risks

- Improperly configured IAM roles could lead to either overly restrictive or permissive access to the S3 bucket.
- Failure to correctly handle the audit logging transactionally could lead to unaudited access.

## 8.4.0.0 Integration Points

- Authentication Service: To verify user role.
- AWS S3 SDK: To generate the pre-signed URL.
- Audit Log Service: To record the access event.
- Project Service: To retrieve project and SOW metadata.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- A System Admin successfully downloads the original SOW.
- A Finance Manager attempts to download the SOW and is denied.
- An Admin attempts to download an SOW for a project where the file is missing in S3.
- The UI correctly disables the download button for a project with no SOW.
- Verify the audit log entry is created with the correct details after a successful download.
- Verify that the generated pre-signed URL expires after its configured lifetime.

## 9.3.0.0 Test Data Needs

- A test project with a valid SOW uploaded.
- A test project with no SOW uploaded.
- User accounts with 'System Administrator' and 'Finance Manager' roles.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- AWS CLI or SDK for manipulating S3 state for edge case testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >= 80% code coverage for new logic
- E2E tests for both happy path and error conditions are implemented and passing
- Security requirements, including RBAC and pre-signed URL expiry, are validated
- The access event is confirmed to be correctly logged in the audit trail in the staging environment
- UI elements are reviewed and approved for accessibility and design consistency
- Documentation for the new API endpoint is created/updated in the OpenAPI specification
- Story deployed and verified in the staging environment by QA and the Product Owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by the completion of US-030 (SOW Upload). It should be prioritized in a sprint immediately following the completion of the upload feature.

## 11.4.0.0 Release Impact

This is a key feature for operational oversight and compliance. Its inclusion is critical for the initial pilot phase (Phase 1) of the rollout.

