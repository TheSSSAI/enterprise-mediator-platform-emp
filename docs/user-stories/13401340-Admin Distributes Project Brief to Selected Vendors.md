# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-042 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Distributes Project Brief to Selected Vendor... |
| As A User Story | As a System Admin, I want to select multiple vendo... |
| User Persona | System Administrator responsible for end-to-end pr... |
| Business Value | Initiates the competitive proposal process, a core... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successful distribution to multiple vendors

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Admin viewing a project with a status of 'Pending' and an approved Project Brief, and the recommended vendor list is displayed

### 3.1.5 When

I select three vendors using the checkboxes, click the 'Distribute Brief' button, and confirm the action in the subsequent modal

### 3.1.6 Then

The system displays a success notification like 'Project Brief successfully distributed to 3 vendors.', the project's status is updated to 'Proposed', and an audit log entry is created for the distribution event.

### 3.1.7 Validation Notes

Verify the project status change in the database. Verify the audit log entry. Verify that three separate asynchronous notification jobs are queued for the selected vendors.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Button state based on vendor selection

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am viewing the recommended vendor list for a project

### 3.2.5 When

I have not selected any vendors via the checkboxes

### 3.2.6 Then

The 'Distribute Brief' button is disabled.

### 3.2.7 Validation Notes

Check the 'disabled' attribute of the button element in the DOM. The button should become enabled as soon as one vendor is selected.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Confirmation modal prevents accidental distribution

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I have selected at least one vendor and clicked the 'Distribute Brief' button

### 3.3.5 When

A confirmation modal appears

### 3.3.6 Then

The modal displays text such as 'You are about to send the Project Brief to X vendor(s). Are you sure you want to proceed?', with 'Confirm' and 'Cancel' buttons.

### 3.3.7 Validation Notes

Clicking 'Cancel' should close the modal with no further action. Clicking 'Confirm' should trigger the distribution process.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempting to distribute brief for a project in an invalid state

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am viewing a project that is already in 'Proposed', 'Awarded', or 'Active' status

### 3.4.5 When

I navigate to the vendor selection screen

### 3.4.6 Then

The 'Distribute Brief' button is not visible or is permanently disabled, and a message indicates that the brief has already been distributed.

### 3.4.7 Validation Notes

Backend API should reject any attempt to distribute for a project not in a valid pre-distribution state, returning a 409 Conflict error.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Backend API failure during distribution

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I have selected vendors and confirmed the distribution

### 3.5.5 When

The backend API call fails due to a server error (e.g., 500)

### 3.5.6 Then

The system displays a user-friendly error message like 'Failed to distribute brief. Please try again.', the project status remains unchanged, and no notifications are sent.

### 3.5.7 Validation Notes

Use browser developer tools to mock a 500 API response and verify the UI handles it gracefully.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Distribution to a vendor with no contact information

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A recommended vendor exists in the system but has no associated contact with a valid email address

### 3.6.5 When

I attempt to select and distribute the brief to this vendor

### 3.6.6 Then

The system should either prevent selection of this vendor (with a tooltip explaining why) or show a validation error upon attempting to distribute, preventing the action from proceeding.

### 3.6.7 Validation Notes

Backend validation must check for the existence of a valid contact email for each selected vendor ID before proceeding.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A list/table of recommended vendors, with each row containing Vendor Name, Similarity Score, and a checkbox.
- A 'Select All' / 'Deselect All' checkbox.
- A primary action button labeled 'Distribute Brief'.
- A confirmation modal with 'Confirm' and 'Cancel' actions.
- Toast notifications for success and error feedback.

## 4.2.0 User Interactions

- User can select/deselect individual vendors or all vendors at once.
- The 'Distribute Brief' button's enabled/disabled state is tied to whether at least one vendor is selected.
- Clicking 'Distribute Brief' opens a confirmation modal.

## 4.3.0 Display Requirements

- The number of selected vendors should be clearly visible (e.g., '3 vendors selected').
- After distribution, the UI should clearly indicate which vendors have been invited (e.g., a status label 'Invited' next to their name).

## 4.4.0 Accessibility Needs

- All checkboxes, buttons, and modal controls must be keyboard accessible and have appropriate ARIA labels.
- The vendor list should be structured as a proper table or list for screen reader navigation.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Project Brief can only be distributed once. Subsequent additions of vendors must be handled through a separate workflow.

### 5.1.3 Enforcement Point

Backend API (Project Service)

### 5.1.4 Violation Handling

The API will return a 409 Conflict error if a distribution request is made for a project with a status of 'Proposed' or later.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A Project Brief can only be distributed to vendors with an 'Active' status.

### 5.2.3 Enforcement Point

Backend API (Project Service)

### 5.2.4 Violation Handling

The API will validate each vendor ID against the Vendor database and reject the request with a 400 Bad Request error if any vendor is not 'Active'.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-036

#### 6.1.1.2 Dependency Reason

A Project Brief must be created and approved before it can be distributed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-039

#### 6.1.2.2 Dependency Reason

The UI for this story is built upon the vendor recommendation list generated in US-039.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-043

#### 6.1.3.2 Dependency Reason

The notification service and email templates for informing vendors must be in place, as this story triggers that functionality.

## 6.2.0.0 Technical Dependencies

- Project Service: For updating project status.
- Notification Service (AWS SES/SQS): For asynchronously sending email notifications.
- User Service: For retrieving vendor contact details.
- Audit Service: For logging the distribution event.

## 6.3.0.0 Data Dependencies

- Requires an existing Project entity in a valid pre-distribution state.
- Requires existing Vendor entities with 'Active' status and valid contact information.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response for the distribution request must be < 250ms (p95).
- The actual email sending process must be handled asynchronously to not block the user interface or the API response.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible by users with the 'System Administrator' role.
- The backend must validate that the project ID belongs to the admin's organization.
- All vendor IDs submitted in the request must be validated against the database.

## 7.3.0.0 Usability

- The process of selecting and distributing should be intuitive, requiring minimal clicks.
- Feedback to the user (success, failure) must be immediate and clear.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend orchestration is required to coordinate multiple services (Project, User, Notification, Audit).
- Implementation of an asynchronous workflow using a message queue (SQS) for reliable email delivery.
- Requires careful state management on the frontend to handle selections and UI feedback.
- Transactional integrity: The project status update and the queuing of notifications should be treated as a single logical unit of work.

## 8.3.0.0 Technical Risks

- Potential for partial failure (e.g., project status updated but notification queuing fails). A robust error handling and potential rollback/retry mechanism is needed.
- Failure in the downstream Notification Service could lead to emails not being sent. Proper monitoring and alerting on the queue depth is essential.

## 8.4.0.0 Integration Points

- Frontend -> API Gateway -> Project Service
- Project Service -> User Service (to get vendor contacts)
- Project Service -> SNS/SQS (to publish 'DistributeBrief' event)
- Project Service -> Audit Service (to log action)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify successful distribution to a single vendor.
- Verify successful distribution to multiple vendors.
- Verify the 'Distribute Brief' button is disabled when no vendors are selected.
- Verify the API rejects requests for projects in an invalid state.
- Verify the UI handles API errors gracefully.
- E2E test: Log in as Admin, navigate to a project, select vendors, distribute, and use a mail-trapping service to confirm emails are generated and sent correctly.

## 9.3.0.0 Test Data Needs

- A test project with an approved brief.
- Multiple test vendors with 'Active' status and valid, testable email addresses.
- A test vendor with an 'Inactive' status.
- A test project in an 'Awarded' state.

## 9.4.0.0 Testing Tools

- Jest (Unit/Integration)
- Playwright (E2E)
- MailHog or similar for email testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage and passing
- E2E test scenario for the happy path is implemented and passing in the CI/CD pipeline
- User interface reviewed and approved for usability and accessibility
- Performance requirements (API latency) verified under test
- Security requirements (role-based access) validated
- Relevant user and technical documentation has been updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical enabler for the entire proposal submission workflow.
- It should be prioritized immediately after the SOW processing and vendor recommendation stories are completed.
- It is a blocker for US-049 (Vendor Submits Proposal).

## 11.4.0.0 Release Impact

- Completing this story enables the core functionality of engaging vendors, which is essential for the Minimum Viable Product (MVP).

