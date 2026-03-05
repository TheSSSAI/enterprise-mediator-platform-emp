# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-038 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Sanitized SOW |
| As A User Story | As a System Administrator, I want to view the PII-... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Mitigates legal and reputational risk by preventin... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Processing |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully view a sanitized SOW

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator viewing a project's detail page, and the associated SOW has been successfully processed and sanitized

### 3.1.5 When

I click on the 'View Sanitized SOW' button or tab

### 3.1.6 Then

The system must display the content of the sanitized SOW in a readable, read-only format, and the content must show placeholders (e.g., '[PERSON_NAME]', '[COMPANY_NAME]') in place of the original PII.

### 3.1.7 Validation Notes

Verify that the displayed text matches the sanitized file stored in S3 and that the UI clearly labels this view as the 'Sanitized Version'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to view a sanitized SOW while processing is in progress

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am a logged-in System Administrator viewing a project's detail page, and the SOW's status is 'PROCESSING'

### 3.2.5 When

I navigate to the SOW management section

### 3.2.6 Then

The 'View Sanitized SOW' option must be disabled, and a status message like 'SOW processing in progress...' should be displayed.

### 3.2.7 Validation Notes

Confirm the button is not clickable and the status message is clear and user-friendly.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to view a sanitized SOW after processing has failed

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in System Administrator viewing a project's detail page, and the SOW's status is 'Failed'

### 3.3.5 When

I navigate to the SOW management section

### 3.3.6 Then

The system must display a prominent error message indicating that 'SOW processing failed' and provide a correlation ID for support.

### 3.3.7 Validation Notes

Check that the error message is displayed instead of the SOW content and that the backend has logged the detailed error.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

View a sanitized SOW where no PII was originally found

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a logged-in System Administrator viewing a project where the SOW was processed successfully but contained no PII

### 3.4.5 When

I click on the 'View Sanitized SOW' button or tab

### 3.4.6 Then

The system must display the SOW content, which will be identical to the original, but it must be clearly labeled as the 'Sanitized Version', and a message like 'Sanitization complete. No PII was detected for removal.' should be visible.

### 3.4.7 Validation Notes

Verify the label and the informational message are present to avoid user confusion.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Access control for sanitized SOW view

### 3.5.3 Scenario Type

Security

### 3.5.4 Given

A user who is not a System Administrator or Finance Manager is attempting to access the sanitized SOW view URL directly

### 3.5.5 When

The user makes a request to the API endpoint for the sanitized SOW

### 3.5.6 Then

The system must return a '403 Forbidden' or '401 Unauthorized' error, and the UI must prevent navigation to this view.

### 3.5.7 Validation Notes

Test this by attempting to access the resource with Client Contact and Vendor Contact roles.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A button, link, or tab labeled 'View Sanitized SOW' within the project's SOW management area.
- A read-only text area or document viewer to display the sanitized content.
- A clear heading or label indicating 'Sanitized Statement of Work'.
- Status indicators for 'Processing' and 'Failed' states.

## 4.2.0 User Interactions

- Clicking the control opens the sanitized SOW view.
- The user can scroll through the entire content of the sanitized SOW.
- The user cannot edit the content in this view.

## 4.3.0 Display Requirements

- The view must clearly differentiate between the original and sanitized versions of the SOW.
- Placeholders for removed PII must be rendered clearly.
- The interface must be responsive and legible on desktop and tablet screen sizes.

## 4.4.0 Accessibility Needs

- The view must adhere to WCAG 2.1 Level AA standards.
- Text must have sufficient color contrast.
- All interactive elements must be keyboard-accessible and have appropriate ARIA labels.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-SOW-01

### 5.1.2 Rule Description

The sanitized SOW can only be viewed after the AI processing workflow has successfully completed.

### 5.1.3 Enforcement Point

API Gateway and Frontend UI

### 5.1.4 Violation Handling

The UI will disable the view option and show a status message. The API will return an appropriate error if accessed directly.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-SEC-05

### 5.2.2 Rule Description

Access to view any SOW (original or sanitized) is restricted to internal user roles (System Administrator, Finance Manager).

### 5.2.3 Enforcement Point

API Gateway and Backend Service Authorization Middleware

### 5.2.4 Violation Handling

The API will return a 403 Forbidden error.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-030

#### 6.1.1.2 Dependency Reason

An SOW must be uploaded to a project before it can be processed and its sanitized version viewed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-032

#### 6.1.2.2 Dependency Reason

The asynchronous SOW processing and sanitization workflow must be implemented to generate the sanitized file that this story displays.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-029

#### 6.1.3.2 Dependency Reason

A project entity must exist to associate the SOW with.

## 6.2.0.0 Technical Dependencies

- AWS S3 bucket for storing the sanitized SOW documents.
- A database schema that links a project to its original and sanitized SOW locations.
- Backend service (Project Service) capable of retrieving file locations from the database.

## 6.3.0.0 Data Dependencies

- A project record in the database with a status indicating the SOW has been processed successfully.
- The corresponding sanitized SOW file must exist in the S3 bucket.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The sanitized SOW content should begin rendering in the UI within 2 seconds of the user's request under normal load.

## 7.2.0.0 Security

- The API endpoint serving the SOW data must be protected and require authentication and role-based authorization.
- Data transfer from S3 to the user must be over HTTPS, potentially using pre-signed S3 URLs with short expiry times to secure access.

## 7.3.0.0 Usability

- The distinction between the original and sanitized SOW must be immediately obvious to the user to prevent errors.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The view must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires both frontend (React component) and backend (NestJS API endpoint) development.
- Backend needs to securely fetch a file from AWS S3 based on a database record.
- Frontend needs to handle multiple states: loading, success, and various error conditions.

## 8.3.0.0 Technical Risks

- Incorrect S3 bucket permissions could block access or expose data.
- Performance issues if fetching and rendering very large SOW documents; consider pagination or streaming for extreme cases.

## 8.4.0.0 Integration Points

- Backend Project Service -> PostgreSQL Database
- Backend Project Service -> AWS S3
- Frontend -> Backend Project Service API

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify a successfully sanitized SOW displays correctly.
- Verify the UI state when SOW processing is incomplete.
- Verify the UI state when SOW processing has failed.
- Verify role-based access control prevents unauthorized users (e.g., Vendor) from viewing the SOW.
- Verify the UI is responsive on different screen sizes.

## 9.3.0.0 Test Data Needs

- A test project with a successfully processed SOW.
- A test project with an SOW in the 'PROCESSING' state.
- A test project with an SOW in the 'Failed' state.
- User accounts with System Admin, Finance Manager, and Vendor Contact roles.

## 9.4.0.0 Testing Tools

- Jest
- Playwright

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage
- E2E tests for critical paths implemented and passing
- User interface reviewed and approved by the Product Owner/designer
- Performance requirements verified (page load < 2s)
- Security requirements validated (RBAC enforced)
- Accessibility audit passed (WCAG 2.1 AA)
- Documentation for the new API endpoint is generated and published
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked until the core SOW upload and AI sanitization pipeline (US-030, US-032) is complete.
- Requires a developer with full-stack capabilities or collaboration between a frontend and backend developer.

## 11.4.0.0 Release Impact

This is a critical feature for the SOW workflow, enabling the quality assurance step before a project brief is sent to vendors. It is essential for the initial pilot release.

