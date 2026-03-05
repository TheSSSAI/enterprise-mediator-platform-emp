# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-045 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Submits Question on Project Proposal Portal |
| As A User Story | As a Vendor Contact reviewing a project opportunit... |
| User Persona | Vendor Contact - An external user who has been inv... |
| Business Value | Increases the quality and accuracy of vendor propo... |
| Functional Area | Proposal and Project Workflow |
| Story Theme | Vendor Proposal Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful Question Submission (Happy Path)

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated Vendor Contact viewing the secure proposal portal for an active project opportunity

### 3.1.5 When

I enter a valid, non-empty question into the 'Ask a Question' text area and click the 'Submit Question' button

### 3.1.6 Then

The system submits the question and I see a success confirmation message, such as 'Your question has been submitted successfully.'

### 3.1.7 Validation Notes

Verify a new record is created in the 'project_questions' table with the correct project_id, vendor_id, question_text, and a status of 'Pending Answer'. Verify the System Admin receives a notification for the new question.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to Submit an Empty Question

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am on the secure proposal portal with the 'Ask a Question' text area visible

### 3.2.5 When

I click the 'Submit Question' button without entering any text

### 3.2.6 Then

The system does not submit the form and I see a validation error message, such as 'Question cannot be empty.'

### 3.2.7 Validation Notes

Check that the 'Submit Question' button is disabled until text is entered. Verify no API call is made if the field is empty upon attempted submission.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to Submit a Question Exceeding Character Limit

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the secure proposal portal

### 3.3.5 When

I enter text exceeding the defined character limit (e.g., 2000 characters) and attempt to submit

### 3.3.6 Then

The system prevents submission and I see a validation error message, such as 'Question cannot exceed 2000 characters.'

### 3.3.7 Validation Notes

The UI should provide a character counter. The backend API must also enforce this limit.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Question Anonymity for Other Vendors

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

Vendor A has submitted a question for Project X

### 3.4.5 When

I, as Vendor B invited to the same Project X, view the Q&A section on the portal

### 3.4.6 Then

I can see the text of the question submitted by Vendor A, but there is no information identifying Vendor A as the author.

### 3.4.7 Validation Notes

Verify the API endpoint that returns the list of questions for a project does not include the vendor_id or any other identifying information in the response payload sent to vendor clients.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Network Failure During Submission

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am on the secure proposal portal and have entered a valid question

### 3.5.5 When

I click 'Submit Question' and a network error occurs

### 3.5.6 Then

I see an error message, such as 'Submission failed. Please check your connection and try again.'

### 3.5.7 And

The text I entered in the question field is preserved.

### 3.5.8 Validation Notes

Simulate a network failure using browser developer tools and verify the UI handles the API call failure gracefully.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Q&A' section on the proposal portal page.
- A multi-line text area for question input, with a placeholder like 'Type your question here...'.
- A 'Submit Question' button.
- A character counter displayed near the text area.
- A toast notification or inline message for success and error feedback.

## 4.2.0 User Interactions

- The 'Submit Question' button should be disabled by default and enabled only when the text area contains text.
- After successful submission, the text area should be cleared.
- The Q&A section should display a list of previously asked questions and their statuses ('Pending Answer', 'Answered').

## 4.3.0 Display Requirements

- The portal must display all questions submitted for the project, along with their corresponding answers (once provided).
- The identity of the vendor who asked the question must never be displayed to other vendors.

## 4.4.0 Accessibility Needs

- The text area must have a proper `<label>` for screen readers.
- All form elements must be navigable and operable via keyboard.
- Validation and confirmation messages must be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Questions submitted by vendors must be anonymous to other vendors but traceable to the originating vendor for internal administrators.

### 5.1.3 Enforcement Point

Backend API and Database Schema

### 5.1.4 Violation Handling

The system must store the `vendor_id` with the question but filter it out of any API responses sent to other vendors.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A vendor can only submit questions for a project to which they have been explicitly invited.

### 5.2.3 Enforcement Point

Backend API Authorization Layer

### 5.2.4 Violation Handling

The API must reject any submission attempt with a 403 Forbidden status code if the authenticated vendor is not associated with the project.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-043

#### 6.1.1.2 Dependency Reason

The vendor must receive an invitation and have access to the secure portal to be able to ask a question.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-044

#### 6.1.2.2 Dependency Reason

The Q&A feature is a component of the project brief portal, which must exist first.

## 6.2.0.0 Technical Dependencies

- A functioning authentication service (AWS Cognito) to identify the vendor.
- A notification service (AWS SES/SNS) to alert the System Admin.
- An event bus (AWS SNS/SQS) for asynchronous communication between the Project Service and Notification Service.

## 6.3.0.0 Data Dependencies

- Requires an existing Project record in the database.
- Requires an existing Vendor record associated with the authenticated user.
- Requires a record linking the Vendor to the Project (e.g., in a `project_invitations` table).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for question submission should be under 500ms (p95).

## 7.2.0.0 Security

- All data must be transmitted over HTTPS.
- All user-submitted text must be sanitized on the backend to prevent Cross-Site Scripting (XSS) attacks.
- The API endpoint must be protected and require authentication and authorization.

## 7.3.0.0 Usability

- The process of asking a question should be intuitive and require minimal steps.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires frontend component development (React).
- Requires a new backend API endpoint with business logic (NestJS).
- Requires database schema modification (new `project_questions` table).
- Requires integration with the notification service via an event bus.

## 8.3.0.0 Technical Risks

- Ensuring the anonymity logic is correctly implemented in the API to prevent accidental data leakage of vendor identities.

## 8.4.0.0 Integration Points

- Project Service API
- Database (PostgreSQL)
- Notification Service (via SNS/SQS)
- Authentication Service (Cognito)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- A vendor successfully submits a question.
- A vendor attempts to submit an empty question.
- A different vendor views the portal and confirms the first vendor's question is visible and anonymous.
- An admin logs in and confirms they received a notification and can see which vendor asked the question.
- An uninvited vendor attempts to submit a question to a project via API manipulation and is blocked.

## 9.3.0.0 Test Data Needs

- At least two distinct vendor accounts.
- A project with both vendors invited.
- A System Administrator account.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage and all passing
- End-to-end tests for the submission and anonymity scenarios are created and passing
- User interface reviewed for UX consistency and approved by the design lead
- Security requirements (input sanitization, authorization) validated
- Accessibility audit passed for the new UI components
- API documentation (OpenAPI) is updated for the new endpoint
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be prioritized alongside US-046 (Admin Receives Notification) and US-047 (Admin Answers Question) to deliver the complete Q&A feature loop.
- Frontend and backend work can be done in parallel after the API contract is defined.

## 11.4.0.0 Release Impact

This is a core feature for the vendor proposal workflow and is critical for enabling effective pre-proposal communication.

