# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-072 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Configures Data Retention Policies |
| As A User Story | As a System Administrator, I want to configure and... |
| User Persona | System Administrator. This is a high-privilege use... |
| Business Value | Enables automated compliance with data protection ... |
| Functional Area | System Administration & Configuration |
| Story Theme | Compliance and Governance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin successfully views and updates a data retention policy

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator and I have navigated to the 'System Settings > Data Retention' page

### 3.1.5 When

I change the retention period for 'Inactive Client Profiles' to '365' days and click 'Save Changes'

### 3.1.6 Then

I should see a success message 'Data retention policies updated successfully.' and the new value of '365 days' is displayed for that policy.

### 3.1.7 Validation Notes

Verify the change is persisted in the database and reflected on the UI after a page refresh.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin is prevented from setting a retention period below a legally mandated minimum

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a logged-in System Administrator on the Data Retention page, and the minimum retention for 'Audit Logs' is hard-coded to 7 years

### 3.2.5 When

I attempt to set the retention period for 'Audit Logs' to '5' years and click 'Save'

### 3.2.6 Then

I should see an inline error message stating 'Retention period for Audit Logs cannot be less than the legal minimum of 7 years.' and the change is not saved.

### 3.2.7 Validation Notes

Test this at the API level to ensure backend validation is enforced.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin enters invalid, non-numeric data into a retention period field

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in System Administrator on the Data Retention page

### 3.3.5 When

I enter '-10' or 'abc' as the retention period for 'Rejected Proposals'

### 3.3.6 Then

The input field should show a validation error like 'Value must be a positive integer.' and the 'Save Changes' button should be disabled.

### 3.3.7 Validation Notes

Verify client-side validation provides immediate feedback.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Changing a retention policy is recorded in the audit trail

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A System Administrator has successfully updated the retention policy for 'Completed Projects' from 730 days to 1095 days

### 3.4.5 When

Another System Administrator views the Audit Trail

### 3.4.6 Then

They should see a new log entry with details: Action='Data Retention Policy Updated', Target Entity='Completed Projects', Before State='730 days', After State='1095 days'.

### 3.4.7 Validation Notes

Check the audit log table in the database for the corresponding entry.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The system's background job correctly enforces a retention policy

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The retention policy for 'Completed Projects' is set to 90 days, and a project was moved to 'Completed' status 91 days ago

### 3.5.5 When

The nightly data retention background job runs

### 3.5.6 Then

The data associated with that completed project should be permanently deleted or archived as defined by the policy, and the job's execution (e.g., 'Deleted 15 project records') is logged.

### 3.5.7 Validation Notes

Requires a test environment with seeded data of varying ages. Verify the correct records are removed and others are untouched.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Admin is shown a confirmation modal before saving changes

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am a logged-in System Administrator on the Data Retention page and have modified a policy

### 3.6.5 When

I click the 'Save Changes' button

### 3.6.6 Then

A confirmation modal appears with a warning: 'Changing retention policies can result in permanent data deletion. Are you sure you want to proceed?' with 'Confirm' and 'Cancel' options.

### 3.6.7 Validation Notes

Verify that clicking 'Cancel' dismisses the modal and does not save the changes.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Data Retention' section within the System Administration settings area.
- A table or list displaying each configurable entity (e.g., 'Completed Projects', 'Audit Logs').
- Numeric input fields for the retention period.
- A dropdown selector for the time unit (Days, Months, Years).
- A 'Save Changes' button, disabled by default.
- A confirmation modal dialog.
- Inline validation error messages.

## 4.2.0 User Interactions

- Admin can modify the numeric value and unit for each policy.
- The 'Save Changes' button becomes active only when a value has been changed.
- Saving requires an explicit confirmation step via the modal.

## 4.3.0 Display Requirements

- Each policy must have a clear label (e.g., 'Inactive Vendor Profiles').
- A brief description for each policy should explain its scope (e.g., 'Period after a vendor is deactivated before their profile is deleted.').
- Policies with a hard-coded minimum value should display this information clearly (e.g., 'Minimum 7 years').

## 4.4.0 Accessibility Needs

- All form fields must have associated labels for screen readers.
- The page must be fully navigable using a keyboard.
- Validation errors must be programmatically associated with their respective input fields.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Certain data entities, such as financial transaction records and audit logs, must have a minimum retention period (e.g., 7 years) to comply with legal standards.

### 5.1.3 Enforcement Point

Backend API validation upon save request.

### 5.1.4 Violation Handling

The API request is rejected with a 400 Bad Request error and a descriptive message. The UI displays this message to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Changes to data retention policies are considered a high-impact security event and must be logged in the immutable audit trail.

### 5.2.3 Enforcement Point

Backend service layer after a successful policy update.

### 5.2.4 Violation Handling

If the audit log write fails, the entire transaction should be rolled back to ensure consistency.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-074

#### 6.1.1.2 Dependency Reason

The System Administrator role and its permissions must be defined to control access to this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-086

#### 6.1.2.2 Dependency Reason

The audit trail system must be implemented to log the policy changes as required by AC-004 and BR-002.

## 6.2.0.0 Technical Dependencies

- A robust background job scheduling and execution system (e.g., AWS EventBridge Scheduler + Lambda, or a cron-like system within Kubernetes).
- A configuration management module in the backend to store and retrieve policy settings.

## 6.3.0.0 Data Dependencies

- The data models for all target entities (Project, Client, Vendor, SOW, etc.) must be finalized, including status fields and timestamps that the retention job will use to identify expired records.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The background job must process records in batches to avoid excessive memory consumption and long-running database transactions.
- The job should be scheduled during off-peak hours to minimize impact on system performance.

## 7.2.0.0 Security

- Access to the data retention configuration page must be strictly limited to the System Administrator role.
- All changes must be logged in the audit trail with before/after states, as per REQ-FUN-005.

## 7.3.0.0 Usability

- The interface must be clear and unambiguous, with helpful descriptions to prevent accidental misconfiguration.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity lies in designing and implementing the background enforcement job. It must be reliable, performant, and fault-tolerant.
- Defining the exact scope of 'data deletion' for each entity is critical. For example, deleting a project may require cascading deletes or anonymization of related records, which requires careful database schema design and business logic.
- The logic must handle timezones correctly when comparing timestamps.

## 8.3.0.0 Technical Risks

- A bug in the background job could lead to unintentional permanent data loss. The job needs extensive testing and logging.
- Poorly optimized queries in the background job could cause database performance degradation.

## 8.4.0.0 Integration Points

- Backend Configuration Service
- Database
- Audit Log Service
- Background Job Scheduler

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify UI validation for various inputs (valid, invalid, boundary values).
- Test API endpoint for saving policies, including permission checks and validation rules.
- Create a dedicated test suite for the background job: seed the database with data of various ages and statuses, run the job, and assert that only the correct records were deleted/archived.
- Verify that a change to a policy is correctly recorded in the audit trail.
- Test access control by attempting to access the page as a non-Admin user.

## 9.3.0.0 Test Data Needs

- A set of user accounts with different roles (Admin, Finance Manager).
- A database state with entities (projects, clients, etc.) having timestamps older than various potential retention periods.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% coverage for new code
- E2E tests for the critical path (updating a policy) are implemented and passing
- The background job has been manually triggered and verified in the staging environment
- Security requirements (access control, audit logging) have been validated
- Administrator documentation for this feature has been created
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has two distinct parts: the UI/API for configuration and the backend job for enforcement. They could potentially be split, but the value is only delivered when both are complete.
- Requires clear definition from the Product Owner on what 'deletion' means for each entity (hard delete vs. soft delete vs. anonymization) before implementation begins.

## 11.4.0.0 Release Impact

This is a foundational feature for long-term compliance and system maintenance. It is a key requirement for meeting GDPR and SOC 2 compliance goals.

