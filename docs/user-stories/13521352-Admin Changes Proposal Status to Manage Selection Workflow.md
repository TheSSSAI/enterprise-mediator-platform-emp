# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-054 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Changes Proposal Status to Manage Selection ... |
| As A User Story | As a System Administrator, I want to change the st... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Enables the core business decision of selecting a ... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin changes a proposal status to 'In Review'

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin is viewing a list of proposals for a project, and a proposal has a status of 'Submitted'

### 3.1.5 When

The Admin changes the status of that proposal to 'In Review'

### 3.1.6 Then

The system updates the proposal's status to 'In Review' in the database, and the UI reflects this change immediately.

### 3.1.7 Validation Notes

Verify the status change in the database and on the proposal management UI. No notification is sent for this internal status change.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin changes a proposal status to 'Shortlisted'

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A System Admin is viewing a list of proposals for a project, and a proposal has a status of 'Submitted' or 'In Review'

### 3.2.5 When

The Admin changes the status of that proposal to 'Shortlisted'

### 3.2.6 Then

The system updates the proposal's status to 'Shortlisted', the UI reflects the change, and a notification is triggered to be sent to the vendor (as per US-055).

### 3.2.7 Validation Notes

Verify the status change in the database, the UI update, and that a 'proposal shortlisted' event is published to the notification service.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin changes a proposal status to 'Rejected'

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A System Admin is viewing a list of proposals for a project

### 3.3.5 When

The Admin changes the status of a proposal to 'Rejected'

### 3.3.6 Then

The system updates the proposal's status to 'Rejected', the UI reflects the change, and a notification is triggered to be sent to the vendor (as per US-055).

### 3.3.7 Validation Notes

Verify the status change in the database, the UI update, and that a 'proposal rejected' event is published to the notification service.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin accepts a proposal, triggering project award

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A System Admin is viewing proposals for a project in 'Proposed' status, and there are at least two proposals with 'Submitted' status

### 3.4.5 When

The Admin changes the status of one proposal to 'Accepted' and confirms the action in a modal dialog

### 3.4.6 Then



```
The system performs the following actions within a single atomic transaction:
1. The selected proposal's status is updated to 'Accepted'.
2. The parent project's status is updated to 'Awarded'.
3. The project record is updated with the accepted vendor's ID.
4. All other proposals for that project have their status automatically changed to 'Rejected'.
5. A 'proposal accepted' notification is triggered for the winning vendor.
6. 'Proposal rejected' notifications are triggered for all other vendors.
7. The entire workflow is logged as a single event in the audit trail.
8. The UI updates to show the new statuses for all proposals and the parent project.
```

### 3.4.7 Validation Notes

Verify all database changes for the project and all associated proposals. Confirm that the correct notification events are published for all vendors. Check the audit log for a detailed entry.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Admin attempts to change status on a proposal for a non-active project

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A project has a status of 'Cancelled' or 'Completed'

### 3.5.5 When

An Admin attempts to change the status of any proposal associated with that project

### 3.5.6 Then

The system prevents the action and displays an informative error message, such as 'Proposal status cannot be changed as the project is no longer active.'

### 3.5.7 Validation Notes

Test with projects in various terminal states ('Cancelled', 'Completed', 'On Hold') to ensure the status change controls are disabled or the API call is rejected.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Confirmation modal for accepting a proposal

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

A System Admin is viewing proposals for a project

### 3.6.5 When

The Admin selects the 'Accept' action for a proposal

### 3.6.6 Then

A confirmation modal appears with the text: 'Accepting this proposal will award the project to [Vendor Name] and automatically reject all other proposals. Are you sure you want to continue?' with 'Confirm' and 'Cancel' buttons.

### 3.6.7 Validation Notes

Verify the modal appears, displays the correct vendor name, and that clicking 'Cancel' closes the modal with no state change.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dropdown menu or button group on each proposal card/row to change its status.
- A confirmation modal dialog for the 'Accept' action.
- Clear visual indicators (e.g., badges, tags) for each proposal's status.

## 4.2.0 User Interactions

- Admin clicks on a control to reveal status options ('In Review', 'Shortlisted', 'Accepted', 'Rejected').
- Selecting 'Accepted' triggers a confirmation modal.
- Confirming the modal triggers the API call and updates the UI for all affected proposals and the project.
- The UI should provide immediate visual feedback (e.g., a loading spinner) while the action is processing.

## 4.3.0 Display Requirements

- The list of available statuses in the dropdown must be dynamically filtered based on the current status (e.g., cannot go from 'Rejected' to 'Shortlisted').
- The confirmation modal must clearly state the consequences of accepting the proposal.

## 4.4.0 Accessibility Needs

- All status change controls must be keyboard accessible (WCAG 2.1 AA).
- The confirmation modal must trap keyboard focus.
- Status indicators must have sufficient color contrast and be accessible to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only one proposal can be 'Accepted' per project.

### 5.1.3 Enforcement Point

Backend service logic, triggered when a proposal status is changed to 'Accepted'.

### 5.1.4 Violation Handling

The system automatically sets all other proposals for the same project to 'Rejected'. This is a core part of the 'Accept' transaction.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Accepting a proposal moves the parent project to the 'Awarded' state.

### 5.2.3 Enforcement Point

Backend service logic, as part of the same transaction as accepting a proposal.

### 5.2.4 Violation Handling

If the project status cannot be updated, the entire transaction fails and is rolled back.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Proposal statuses can only be modified while the parent project is in the 'Proposed' state.

### 5.3.3 Enforcement Point

API endpoint validation.

### 5.3.4 Violation Handling

The API request is rejected with a 400-level error code and a descriptive message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-052

#### 6.1.1.2 Dependency Reason

This story implements the action on the UI provided by the Proposal Comparison View.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-049

#### 6.1.2.2 Dependency Reason

Requires proposals to exist in a 'Submitted' state to be acted upon.

## 6.2.0.0 Technical Dependencies

- Project Service: To update the parent project's status and awarded vendor ID.
- Notification Service: To trigger email notifications to vendors.
- Audit Service: To log the status change event.
- Database: Requires support for atomic transactions to ensure data consistency.

## 6.3.0.0 Data Dependencies

- Requires a project entity with an associated list of proposal entities, each linked to a vendor.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to change a status, including all database transactions, must complete with a p95 latency of < 250ms (as per REQ-NFR-001).

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible by users with the 'System Administrator' role (as per REQ-SEC-001).
- All status changes, especially 'Accepted' and 'Rejected', must be logged in the immutable audit trail with user ID, timestamp, and affected entities (as per REQ-FUN-005).

## 7.3.0.0 Usability

- The consequence of accepting a proposal must be made explicitly clear to the user via a confirmation dialog to prevent accidental project awards.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (as per REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The backend logic for the 'Accept' action requires a database transaction that spans multiple tables (proposals, projects) to ensure atomicity.
- Integration with the asynchronous notification service requires robust error handling and potential retry logic.
- Frontend state management is non-trivial, as a single action can affect the state of multiple items on the screen (all proposals and the project header).

## 8.3.0.0 Technical Risks

- Potential for race conditions if two admins try to act on the same project's proposals simultaneously. The transactional logic should mitigate this.
- Failure in the notification service could lead to vendors not being informed. The system should be resilient to this (e.g., using a durable queue).

## 8.4.0.0 Integration Points

- Backend API for updating proposal status.
- Backend API for the Project service.
- Event bus (e.g., AWS SNS/SQS) for the Notification service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify each status change ('In Review', 'Shortlisted', 'Rejected') works correctly.
- Thoroughly test the 'Accept' workflow: confirm the accepted proposal, the rejected proposals, and the parent project all have the correct final statuses.
- Test the error case where the project is not in a 'Proposed' state.
- Verify that notifications are correctly triggered for each relevant status change.
- Verify the audit log entry is created with the correct details.

## 9.3.0.0 Test Data Needs

- A test project in 'Proposed' status.
- At least three proposals from three different vendors associated with the test project, all in 'Submitted' status.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% code coverage for the new logic
- E2E tests for the 'Accept' workflow created and passing
- User interface reviewed and approved by the product owner/designer
- API endpoint security (role-based access) is verified
- Audit logging is confirmed to be working as specified
- No performance regressions introduced
- Documentation for the API endpoint is updated in the OpenAPI specification
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical enabler for the financial workflow (invoicing). It should be prioritized accordingly.
- Should be developed in the same sprint as US-055 (Vendor Receives Proposal Status Update) due to their tight coupling.

## 11.4.0.0 Release Impact

This feature is essential for the core business workflow and is required for the initial product launch.

