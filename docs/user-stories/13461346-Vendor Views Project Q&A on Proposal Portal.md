# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-048 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Views Project Q&A on Proposal Portal |
| As A User Story | As a Vendor Contact invited to a project, I want t... |
| User Persona | Vendor Contact - An external user who has been inv... |
| Business Value | Ensures a fair and transparent bidding process by ... |
| Functional Area | Proposal and Project Workflow |
| Story Theme | Vendor Proposal Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Vendor views a populated Q&A list

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a Vendor Contact has accessed the secure proposal submission portal for a project

### 3.1.5 When

they navigate to the 'Project Q&A' section and there are answered questions

### 3.1.6 Then

a list of all question-and-answer pairs for that project is displayed.

### 3.1.7 And

the list is sorted with the most recently answered questions at the top.

### 3.1.8 Validation Notes

Verify the API response does not contain the submitting vendor's ID. Verify the UI renders the list in reverse chronological order of the answer timestamp.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Vendor views an empty Q&A list

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

a Vendor Contact has accessed the secure proposal submission portal for a project

### 3.2.5 When

they navigate to the 'Project Q&A' section and no questions have been asked yet

### 3.2.6 Then

a clear message is displayed, such as 'No questions have been asked for this project yet.'

### 3.2.7 Validation Notes

Test with a new project that has zero associated Q&A records in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Vendor views a Q&A list with unanswered questions

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

a Vendor Contact has accessed the secure proposal submission portal for a project

### 3.3.5 When

they view the 'Project Q&A' section and a question has been submitted but not yet answered

### 3.3.6 Then

the question text is visible in the list.

### 3.3.7 And

the question is clearly marked with a status, such as 'Awaiting Answer', in place of an answer.

### 3.3.8 Validation Notes

Verify that questions with a 'Pending' status and a null answer are rendered correctly in the UI.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

UI handles long text gracefully

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a Vendor Contact is viewing the Q&A list

### 3.4.5 When

a question or answer contains long text with multiple paragraphs

### 3.4.6 Then

the text is formatted with proper line breaks and wrapping to ensure full readability without breaking the UI layout.

### 3.4.7 Validation Notes

Test with a Q&A entry containing at least 500 characters and multiple newline characters.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Vendor cannot access Q&A for other projects

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

a Vendor Contact is authenticated and has access to the proposal portal for Project A

### 3.5.5 When

they attempt to make an API call to fetch the Q&A for Project B (to which they were not invited)

### 3.5.6 Then

the API returns a '403 Forbidden' or '404 Not Found' error.

### 3.5.7 Validation Notes

This requires an automated test that attempts to call the Q&A endpoint with a valid token but for a project ID the user is not associated with.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section or tab labeled 'Project Q&A' within the proposal submission portal.
- A list container for displaying Q&A pairs.
- Individual 'cards' or list items for each Q&A pair.
- A placeholder message for when the Q&A list is empty.

## 4.2.0 User Interactions

- The user can scroll through the list of questions and answers.
- The view is read-only for the vendor.

## 4.3.0 Display Requirements

- Each Q&A item must display the anonymized question, the official answer, and the timestamp of the answer.
- Unanswered questions must display the question and a status indicator (e.g., 'Awaiting Answer').
- The list should be sorted in reverse chronological order based on the answer timestamp.

## 4.4.0 Accessibility Needs

- The Q&A list must be navigable via keyboard.
- Questions and answers should be properly associated using ARIA attributes for screen reader users.
- Adherence to WCAG 2.1 Level AA for color contrast and text size.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The identity of the vendor who submits a question must always be kept anonymous from other vendors.

### 5.1.3 Enforcement Point

API Gateway and Backend Service (Project Service).

### 5.1.4 Violation Handling

The API response must never include the 'submitted_by_vendor_id' field or any other identifying information.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only questions and answers pertaining to the specific project being viewed are accessible.

### 5.2.3 Enforcement Point

Backend Service (Project Service).

### 5.2.4 Violation Handling

The database query must be strictly filtered by the project ID derived from the authenticated user's context.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-043

#### 6.1.1.2 Dependency Reason

Vendor needs the secure link from the invitation to access the portal.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-044

#### 6.1.2.2 Dependency Reason

Defines the proposal submission portal where the Q&A feature will reside.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-045

#### 6.1.3.2 Dependency Reason

Defines the mechanism for vendors to submit questions, which are the source of data for this story.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-047

#### 6.1.4.2 Dependency Reason

Defines the mechanism for Admins to answer questions, which populates the answers viewed in this story.

## 6.2.0.0 Technical Dependencies

- A secure, token-based authentication system (e.g., JWT via AWS Cognito).
- An API endpoint to fetch Q&A data for a specific project.

## 6.3.0.0 Data Dependencies

- A data model/table for storing Q&A records, including `project_id`, `question_text`, `answer_text`, `status`, and timestamps.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The Q&A list should load within 1 second for up to 100 Q&A pairs.
- API response time (p95) for the Q&A endpoint must be under 250ms.

## 7.2.0.0 Security

- Access to the Q&A endpoint must be restricted to authenticated users with explicit permission for the requested project.
- The API must not leak any Personally Identifiable Information (PII) about the vendor who asked the question.

## 7.3.0.0 Usability

- The Q&A list must be clear, legible, and easy to scan.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires a new read-only API endpoint with straightforward authorization logic.
- Requires a new frontend component to display a list of data.
- Data model is simple and likely already defined by dependent stories.

## 8.3.0.0 Technical Risks

- Ensuring the authorization logic is correctly implemented to prevent data leakage between projects is critical.

## 8.4.0.0 Integration Points

- Frontend proposal portal.
- Backend Project Service API.
- PostgreSQL database.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a vendor can see answered questions for their invited project.
- Verify the 'empty' state message appears when no questions exist.
- Verify unanswered questions are displayed with a 'pending' status.
- Verify a vendor cannot access the Q&A for a project they were not invited to.
- Verify the API response never contains the submitting vendor's ID.

## 9.3.0.0 Test Data Needs

- A project with multiple answered Q&A records.
- A project with zero Q&A records.
- A project with a mix of answered and unanswered questions.
- At least two vendor accounts and two separate projects to test access control.

## 9.4.0.0 Testing Tools

- Jest for frontend/backend unit tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >80% coverage and passing
- Integration testing completed successfully
- E2E tests for key scenarios are passing in the CI/CD pipeline
- User interface reviewed and approved for UX and accessibility
- Performance requirements verified
- Security review of the API endpoint completed and any findings addressed
- Documentation for the new API endpoint is created/updated in OpenAPI spec
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be planned in the same sprint as US-045 (Vendor Submits Question) and US-047 (Admin Answers Question) to complete the Q&A feature loop.

## 11.4.0.0 Release Impact

This is a core component of the initial proposal management feature set.

