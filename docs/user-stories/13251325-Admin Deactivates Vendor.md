# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-027 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Deactivates Vendor |
| As A User Story | As a System Administrator, I want to change a vend... |
| User Persona | System Administrator (as defined in REQ-SEC-001) |
| Business Value | Enables effective vendor lifecycle management, mai... |
| Functional Area | Entity Management |
| Story Theme | Vendor Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully deactivates an active vendor

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator viewing the details page of a vendor with 'Active' status

### 3.1.5 When

I click the 'Deactivate Vendor' button and confirm the action in the confirmation dialog

### 3.1.6 Then

The system updates the vendor's status to 'Deactivated' in the database, a success notification 'Vendor [Vendor Name] has been deactivated.' is displayed, and the vendor's profile page reflects the new 'Deactivated' status.

### 3.1.7 Validation Notes

Verify the status change in the UI and by checking the database. Confirm the success toast message appears. Check the audit log for the corresponding entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Deactivated vendor is excluded from new project matching

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A vendor has been successfully deactivated

### 3.2.5 When

A System Administrator creates a new project and the system generates a list of recommended vendors

### 3.2.6 Then

The deactivated vendor does not appear in the recommended vendor list or in any vendor search results for new project assignments.

### 3.2.7 Validation Notes

Create a new project and trigger the vendor matching process (REQ-FUN-002). Verify the deactivated vendor is absent from the results. This tests the integration with the search/matching service.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin cancels the deactivation process

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am a logged-in System Administrator and have opened the deactivation confirmation dialog for a vendor

### 3.3.5 When

I click the 'Cancel' button

### 3.3.6 Then

The confirmation dialog closes, the vendor's status remains 'Active', and no changes are saved.

### 3.3.7 Validation Notes

Verify that the vendor's status in the UI and database is unchanged and that no audit log entry was created for this action.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Deactivating a vendor with active projects does not affect those projects

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A vendor with 'Active' status is currently awarded to one or more projects with a status of 'Awarded' or 'Active'

### 3.4.5 When

A System Administrator deactivates that vendor

### 3.4.6 Then

The vendor's status is changed to 'Deactivated', but their association with the ongoing projects remains intact, and they remain eligible for payouts related to those specific projects.

### 3.4.7 Validation Notes

Check the project details page for an active project assigned to the now-deactivated vendor. Verify the vendor is still listed as the awarded vendor. Verify that the payout workflow for that project is not blocked.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Deactivating a vendor with open proposals automatically withdraws them

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A vendor with 'Active' status has submitted a proposal for a project that is still in the 'Proposed' stage

### 3.5.5 When

A System Administrator deactivates that vendor

### 3.5.6 Then

The vendor's status is changed to 'Deactivated', and their open proposal for the 'Proposed' project is automatically updated to a 'Withdrawn' status.

### 3.5.7 Validation Notes

Check the proposal dashboard for the relevant project (REQ-FUN-003). The proposal from the deactivated vendor should now show as 'Withdrawn' or a similar terminal state.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Non-admin user cannot see or perform the deactivation action

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am logged in as a user with a role other than System Administrator (e.g., Finance Manager)

### 3.6.5 When

I view a vendor's profile page

### 3.6.6 Then

The 'Deactivate Vendor' button or option is not visible or is disabled, and any direct API call to the deactivation endpoint returns a 403 Forbidden status.

### 3.6.7 Validation Notes

Log in with test accounts for each non-admin role and verify the UI. Use an API client like Postman to attempt a direct API call and assert the 403 response.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Vendor deactivation is logged in the audit trail

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

A System Administrator is about to deactivate a vendor

### 3.7.5 When

The deactivation is successfully confirmed

### 3.7.6 Then

A new entry is created in the immutable audit trail containing the timestamp, the responsible Admin's user ID, the action ('Vendor Deactivated'), the target vendor ID, and the before/after state of the status field ('Active' -> 'Deactivated').

### 3.7.7 Validation Notes

After deactivating a vendor, query the audit log table or use the audit trail UI (US-086) to find and verify the contents of the new log entry.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Deactivate Vendor' button on the Vendor Details page.
- A confirmation modal dialog with 'Deactivate' and 'Cancel' buttons.
- A status indicator/badge on the vendor profile showing 'Deactivated'.
- A toast notification component for success/error messages.

## 4.2.0 User Interactions

- Clicking 'Deactivate Vendor' opens the confirmation modal.
- The confirmation modal must be dismissed (by clicking 'Deactivate' or 'Cancel') before other UI interactions can proceed.
- The confirmation text must clearly state the consequences (e.g., 'This will prevent the vendor from being matched with new projects. Active projects will not be affected. Are you sure?').

## 4.3.0 Display Requirements

- The vendor's status must be clearly visible on their profile and in the main vendor list.
- The vendor list should be filterable by 'Deactivated' status (as per US-023).

## 4.4.0 Accessibility Needs

- The confirmation dialog must be keyboard-navigable and properly managed for screen reader focus.
- All buttons and controls must have accessible names (aria-labels).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-VEND-001

### 5.1.2 Rule Description

Deactivating a vendor only prevents them from being matched with or awarded NEW projects.

### 5.1.3 Enforcement Point

During vendor matching (REQ-FUN-002) and project award workflows.

### 5.1.4 Violation Handling

Deactivated vendors are filtered out of any queries for eligible vendors.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-VEND-002

### 5.2.2 Rule Description

Deactivation does not terminate or alter existing, in-flight project contracts or financial obligations.

### 5.2.3 Enforcement Point

Financial and Project Management modules.

### 5.2.4 Violation Handling

System logic for payouts and project management must not check for 'Deactivated' status on already-awarded projects.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-PROP-001

### 5.3.2 Rule Description

All open (not accepted or rejected) proposals from a vendor must be automatically withdrawn upon their deactivation.

### 5.3.3 Enforcement Point

On successful completion of the vendor deactivation action.

### 5.3.4 Violation Handling

An event is published ('VendorDeactivated') which triggers a listener in the Project/Proposal service to update the status of relevant proposals.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-024

#### 6.1.1.2 Dependency Reason

Requires the Vendor Details view to exist as a location for the 'Deactivate' button.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-074

#### 6.1.2.2 Dependency Reason

Requires the Role-Based Access Control (RBAC) system to be in place to restrict this action to System Administrators.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

Requires the audit trail system to be functional to log this critical action.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-039

#### 6.1.4.2 Dependency Reason

The vendor recommendation/matching logic must be updated to respect the 'Deactivated' status, making this a key integration point.

## 6.2.0.0 Technical Dependencies

- Vendor Management Service
- Authentication Service (for RBAC)
- Audit Logging Service
- Asynchronous Event Bus (e.g., AWS SNS/SQS) for inter-service communication

## 6.3.0.0 Data Dependencies

- The Vendor entity must have a 'status' attribute that can be updated.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The deactivation API call should complete in under 500ms (p95).
- The asynchronous update of proposals and search indexes should complete within 5 seconds.

## 7.2.0.0 Security

- The API endpoint for deactivation must be protected and only accessible to users with the 'System Administrator' role (REQ-NFR-003).
- The action must be logged in the immutable audit trail (REQ-FUN-005).

## 7.3.0.0 Usability

- The action must have a confirmation step to prevent accidental deactivation.
- Feedback (success/error notification) must be immediate and clear.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported browsers (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity is not the status change itself, but managing the side effects across multiple microservices.
- Requires event-driven communication to decouple the Vendor service from the Project and Search services.
- The logic to find and update open proposals for the deactivated vendor needs to be robust.

## 8.3.0.0 Technical Risks

- Potential for inconsistency if an event handler fails. Event handlers must be idempotent and include retry logic.
- Failure to correctly update the search index could lead to deactivated vendors still appearing in recommendations.

## 8.4.0.0 Integration Points

- Vendor Service (source of action)
- Project/Proposal Service (subscribes to 'VendorDeactivated' event to update proposals)
- Search Service (subscribes to 'VendorDeactivated' event to update search index)
- Audit Service (logs the action)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Deactivate a vendor with no active work.
- Deactivate a vendor with an active project and verify the project is unaffected.
- Deactivate a vendor with an open proposal and verify the proposal is withdrawn.
- Attempt deactivation with a non-admin user and verify failure.
- Verify the audit log entry is created correctly.
- Create a new project after deactivation and verify the vendor is not recommended.

## 9.3.0.0 Test Data Needs

- Test users with 'System Administrator' and 'Finance Manager' roles.
- Vendor profiles in 'Active' state.
- Vendors associated with active projects.
- Vendors who have submitted proposals to projects that are not yet awarded.

## 9.4.0.0 Testing Tools

- Jest (Unit/Integration)
- Playwright (E2E)
- Postman (API testing)

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are written with >80% code coverage for new logic.
- E2E tests covering the critical path have been created and are passing.
- The action is correctly logged in the audit trail.
- The vendor is correctly excluded from new project matching.
- Any related documentation (e.g., admin guide) has been updated.
- The feature has been successfully deployed to the staging environment and verified by QA.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational administrative feature required for proper system governance.
- Requires coordination between developers working on the Vendor, Project, and Search services.
- The event schema for 'VendorDeactivated' must be defined and agreed upon by all consuming services before implementation begins.

## 11.4.0.0 Release Impact

This feature is a core component of the Vendor Management functionality and is likely required for the initial release.

