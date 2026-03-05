# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-047 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Answers Vendor Question and Publishes to All... |
| As A User Story | As a System Administrator, I want to provide a sin... |
| User Persona | System Administrator |
| Business Value | Ensures a level playing field for all vendors by p... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully answers a question

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Admin logged in and viewing the Q&A section of a project that has an unanswered question from a vendor

### 3.1.5 When

I enter a non-empty answer into the designated input field for that question and click the 'Submit Answer' button

### 3.1.6 Then

The system saves the answer and associates it with the question in the database.

### 3.1.7 And

The UI updates to show the question with its new answer.

### 3.1.8 Validation Notes

Verify in the database that the 'answer_text' and 'status' fields are updated for the question record. Verify the success message appears on the UI.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Answer is visible to all invited vendors

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A System Admin has successfully answered a question for Project 'Alpha'

### 3.2.5 When

A vendor who was invited to Project 'Alpha' views the project's Q&A portal

### 3.2.6 Then

The vendor can see the question and the complete answer provided by the Admin.

### 3.2.7 And

The identity of the vendor who asked the question is not displayed.

### 3.2.8 Validation Notes

Log in as an invited vendor and navigate to the project portal. Confirm the Q&A pair is visible and anonymous. Use Playwright for E2E test.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Answer is not visible to uninvited vendors

### 3.3.3 Scenario Type

Security_Condition

### 3.3.4 Given

A System Admin has successfully answered a question for Project 'Alpha'

### 3.3.5 When

A vendor who was NOT invited to Project 'Alpha' (but may be invited to other projects) views their dashboard

### 3.3.6 Then

The question and answer for Project 'Alpha' are not visible to them in any context.

### 3.3.7 Validation Notes

Log in as a vendor not associated with the project and confirm that no information about Project Alpha's Q&A is accessible.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin attempts to submit an empty answer

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a System Admin attempting to answer a question

### 3.4.5 When

I leave the answer input field empty and click 'Submit Answer'

### 3.4.6 Then

The system prevents the submission.

### 3.4.7 And

The question remains in an 'Unanswered' state.

### 3.4.8 Validation Notes

Check for a client-side validation message and verify that no API call is made, or if it is, the server returns a 400-level error and the database is not updated.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

API endpoint for answering is secure

### 3.5.3 Scenario Type

Security_Condition

### 3.5.4 Given

A user who is not a System Admin is authenticated

### 3.5.5 When

They attempt to make a direct API call to the endpoint for submitting an answer

### 3.5.6 Then

The API responds with an authorization error (e.g., 403 Forbidden).

### 3.5.7 Validation Notes

Use an API testing tool like Postman or an integration test to make a request with a non-Admin user token and assert the 403 response.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A list/section displaying unanswered questions within the Admin's project workspace.
- A multi-line text area for composing the answer.
- A 'Submit Answer' button.
- A visual indicator to distinguish between 'Answered' and 'Unanswered' questions.
- A read-only view on the Vendor Portal to display Q&A pairs.

## 4.2.0 User Interactions

- Admin clicks on an unanswered question to reveal the answer input form.
- After submission, a success toast/notification appears briefly.
- The Q&A list on the Admin's view updates to reflect the new 'Answered' status.

## 4.3.0 Display Requirements

- The full text of the vendor's question must be displayed to the Admin.
- The full text of the question and its corresponding answer must be displayed on the Vendor Portal.
- Timestamps for when the question was asked and when it was answered should be displayed.

## 4.4.0 Accessibility Needs

- The answer text area must have an associated <label>.
- All interactive elements (buttons, links) must be keyboard-focusable and operable.
- The Q&A list must be structured semantically (e.g., using <dl>, <dt>, <dd>) to be navigable by screen readers, in compliance with REQ-INT-001 (WCAG 2.1 AA).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

An answer, once submitted, is considered final and cannot be edited through the standard user interface.

### 5.1.3 Enforcement Point

User Interface

### 5.1.4 Violation Handling

The UI will not present an 'Edit' or 'Delete' option for an answer that has been published.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Answers can only be provided for projects in a pre-award status (e.g., 'Proposed').

### 5.2.3 Enforcement Point

API Endpoint

### 5.2.4 Violation Handling

If an attempt is made to answer a question for a project that is already 'Awarded' or 'Completed', the API will return an error indicating the action is not allowed.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-045

#### 6.1.1.2 Dependency Reason

A mechanism for vendors to submit questions must exist before an admin can answer them.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-046

#### 6.1.2.2 Dependency Reason

An admin needs to be aware of new questions, which this story's notification system provides.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-048

#### 6.1.3.2 Dependency Reason

A view for vendors to see the project Q&A must exist to display the published answer.

## 6.2.0.0 Technical Dependencies

- Project Service for managing project data and state.
- User Service (or AWS Cognito) for authenticating and authorizing the System Admin.
- Database schema for storing questions and answers (e.g., a 'questions' table with an 'answer_text' column).

## 6.3.0.0 Data Dependencies

- Requires an existing project with a 'Proposed' status.
- Requires at least one vendor to be invited to the project.
- Requires at least one unanswered question associated with that project.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to submit an answer must adhere to the system-wide p95 latency of < 250ms (REQ-NFR-001).

## 7.2.0.0 Security

- The API endpoint for submitting answers must be protected and only accessible by users with the 'System Administrator' role (REQ-SEC-001).
- All user-generated content (the answer text) must be sanitized before rendering in the browser to prevent XSS attacks (REQ-NFR-003).

## 7.3.0.0 Usability

- The process of finding and answering a question should be intuitive, requiring minimal clicks from the project dashboard.

## 7.4.0.0 Accessibility

- All UI components must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- Functionality must be verified on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires changes in both the Admin frontend and the Vendor-facing frontend.
- Involves a new API endpoint with business logic for state validation.
- Requires careful handling of data permissions to ensure Q&A is only visible to the correct vendors.
- Consider emitting a `QUESTION_ANSWERED` event to the event bus (SNS/SQS) to allow the Notification Service to act on it in a decoupled manner (e.g., for a future story about notifying vendors of new answers).

## 8.3.0.0 Technical Risks

- Potential for race conditions if the project status changes while an admin is typing an answer. The backend must re-verify project status upon submission.
- Ensuring real-time updates on the vendor portal could add complexity; a simple page-reload approach is sufficient for the initial implementation.

## 8.4.0.0 Integration Points

- Admin React Application (Project Workspace UI)
- Vendor Portal React Application (Q&A View)
- Backend Project Service (API endpoint and business logic)
- PostgreSQL Database (Data persistence)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Admin successfully answers a question.
- Vendor A (invited) can see the answer.
- Vendor B (not invited) cannot see the answer.
- Admin cannot submit an empty answer.
- A non-admin user cannot call the answer submission API.

## 9.3.0.0 Test Data Needs

- A test user with 'System Administrator' role.
- At least two test users with 'Vendor Contact' role.
- A project in 'Proposed' status with one vendor invited and one not invited.
- An unanswered question linked to the test project.

## 9.4.0.0 Testing Tools

- Jest for unit and integration tests.
- Playwright for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit test coverage for new logic is at or above the 80% project standard.
- End-to-end tests covering the full Q&A visibility workflow are implemented and passing.
- UI changes have been reviewed for consistency and adherence to accessibility standards.
- API endpoint is documented in the OpenAPI specification.
- Security checks (permission enforcement, input sanitization) have been verified.
- Story has been demonstrated to the Product Owner and accepted.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a key part of the proposal workflow and unblocks further vendor interaction features. It should be prioritized after its prerequisite stories are complete.

## 11.4.0.0 Release Impact

Completes the core Q&A loop, which is a critical feature for the initial release of the proposal management module.

