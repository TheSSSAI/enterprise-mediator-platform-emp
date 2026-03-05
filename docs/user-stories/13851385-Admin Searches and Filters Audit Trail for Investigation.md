# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-087 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Searches and Filters Audit Trail for Investi... |
| As A User Story | As a System Administrator, I want to search and fi... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Enables critical security forensics, compliance au... |
| Functional Area | Security and Auditing |
| Story Theme | System Governance and Compliance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Filter by a specific user

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Administrator logged in and viewing the Audit Trail page

### 3.1.5 When

I select a specific user from the user filter and click 'Apply'

### 3.1.6 Then

The audit trail results list updates to show only log entries initiated by the selected user, and the results are paginated.

### 3.1.7 Validation Notes

Verify the API request includes the correct 'userId' parameter. Check the response data to ensure all entries belong to the selected user.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Filter by a date range

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a System Administrator logged in and viewing the Audit Trail page

### 3.2.5 When

I select a start date and an end date in the date range picker and click 'Apply'

### 3.2.6 Then

The audit trail results list updates to show only log entries with a timestamp falling within the selected date range (inclusive).

### 3.2.7 Validation Notes

Test with various date ranges, including single-day and multi-month ranges. Ensure timezone handling is consistent.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Filter by a specific action type

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am a System Administrator logged in and viewing the Audit Trail page

### 3.3.5 When

I select one or more action types (e.g., 'PROJECT_CREATED', 'USER_LOGIN_FAILED') from the action filter and click 'Apply'

### 3.3.6 Then

The audit trail results list updates to show only log entries that match the selected action types.

### 3.3.7 Validation Notes

Verify the API request includes the correct 'actionType' parameter(s). Check the response data for correctness.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Combine multiple filters

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am a System Administrator logged in and viewing the Audit Trail page

### 3.4.5 When

I select a user, a date range, and an action type, and then click 'Apply'

### 3.4.6 Then

The audit trail results list updates to show only log entries that satisfy all the specified filter criteria.

### 3.4.7 Validation Notes

Test multiple combinations of filters to ensure the backend query logic (AND conditions) is correct.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

No results found for filter criteria

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a System Administrator logged in and viewing the Audit Trail page

### 3.5.5 When

I apply a combination of filters that results in zero matching log entries

### 3.5.6 Then

The results area displays a clear and user-friendly message, such as 'No audit logs found matching your criteria.'

### 3.5.7 Validation Notes

Create a filter combination that is guaranteed to return no results and verify the message is displayed instead of an empty table.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Resetting all active filters

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am a System Administrator on the Audit Trail page with one or more filters applied

### 3.6.5 When

I click the 'Reset' or 'Clear Filters' button

### 3.6.6 Then

All filter input fields are cleared to their default state, and the audit trail results list reverts to showing the most recent, unfiltered log entries.

### 3.6.7 Validation Notes

Verify that UI controls are reset and a new API call is made without filter parameters.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Attempting to search with an invalid date range

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am a System Administrator on the Audit Trail page

### 3.7.5 When

I select a start date that is after the end date in the date range picker

### 3.7.6 Then

A validation error message is displayed next to the date picker, and the 'Apply' button is disabled until the error is corrected.

### 3.7.7 Validation Notes

Check for client-side validation preventing the API call.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Viewing detailed log entry data

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

The audit trail results are displayed

### 3.8.5 When

I click on a 'View Details' icon or expand a result row

### 3.8.6 Then

A modal or expanded view appears, showing the full 'Before' and 'After' state snapshots for that log entry in a readable format (e.g., formatted JSON).

### 3.8.7 Validation Notes

Verify that the detailed data matches the 'Before/After State Snapshot' field from the Audit Log entity (REQ-DAT-001).

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Date range picker with start and end date inputs.
- Autocomplete search box or dropdown for selecting a User.
- Multi-select dropdown for selecting Action Types.
- Primary 'Apply Filters' button.
- Secondary 'Reset' button.
- A data table to display results with sortable columns for Timestamp, User, Action, Target Entity, and IP Address.
- Pagination controls (Next, Previous, Page Number).
- Loading indicator shown during data fetch.
- A modal or expandable row component for displaying detailed log data.

## 4.2.0 User Interactions

- Applying filters triggers an API call and updates the results table.
- Clicking on table headers sorts the current view of the data.
- Changing pages via pagination controls fetches the corresponding set of results.

## 4.3.0 Display Requirements

- Timestamps must be displayed in the user's local timezone or a consistent system-wide timezone (e.g., UTC) with the timezone specified.
- User should be displayed by name and/or email.
- The 'Before/After' state data should be clearly differentiated and formatted for readability.

## 4.4.0 Accessibility Needs

- All form controls (date picker, dropdowns, buttons) must be fully keyboard accessible and have appropriate ARIA labels.
- The results table must use proper semantic HTML (`<table>`, `<thead>`, `<th>`, `<tbody>`) for screen reader compatibility.
- Adherence to WCAG 2.1 Level AA standards is required (REQ-INT-001).

# 5.0.0 Business Rules

- {'rule_id': 'BR-SEC-01', 'rule_description': "Access to the Audit Trail feature (read, search) is restricted to users with the 'System Administrator' role.", 'enforcement_point': 'API Gateway and Backend Service Middleware.', 'violation_handling': "The API will return a 403 Forbidden status code. The UI will redirect the user to an 'Access Denied' page or back to their dashboard."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-086', 'dependency_reason': 'This story adds search/filter functionality to the basic audit trail view established in US-086. The foundational UI and data fetching mechanism must exist first.'}

## 6.2.0 Technical Dependencies

- A functioning audit logging service that captures events from all microservices and stores them with the required attributes (REQ-DAT-001).
- An Elasticsearch/OpenSearch cluster (REQ-TEC-003) where audit logs are indexed for efficient searching.
- An API endpoint in the User Service to fetch a list of all users (active and inactive) for the filter dropdown.
- The system's Authentication/Authorization service (AWS Cognito) to enforce role-based access control.

## 6.3.0 Data Dependencies

- A predefined and consistent taxonomy of 'Action Types' must be established and used across all microservices to make the action filter meaningful.
- Availability of historical audit log data in the staging environment for testing.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- 95th percentile (p95) API response time for search queries must be less than 500ms, even with millions of log entries. This is a slightly relaxed target from the standard 250ms due to potential query complexity.
- The UI must remain responsive while data is being fetched.

## 7.2.0 Security

- All API requests to search the audit trail must be authenticated and authorized.
- Input from filter controls must be sanitized on the backend to prevent injection attacks (e.g., Elasticsearch query injection).

## 7.3.0 Usability

- The filter controls should be intuitive and easy to use.
- Error messages must be clear and actionable.

## 7.4.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Backend complexity in constructing robust and performant Elasticsearch queries from the various filter inputs.
- Potential performance challenges if the audit log database is not properly indexed or if Elasticsearch is not used.
- Frontend state management for handling multiple filter states, pagination, and loading indicators.

## 8.3.0 Technical Risks

- Slow query performance if the data volume is very large and the Elasticsearch index/query is not optimized.
- Inconsistent 'Action Type' strings logged by different microservices could make the action filter unreliable.

## 8.4.0 Integration Points

- Backend service must query the Elasticsearch/OpenSearch service.
- Backend service must query the User Service to populate the user filter.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security
- Accessibility

## 9.2.0 Test Scenarios

- Test each filter individually.
- Test various combinations of 2 and 3 filters.
- Test filtering with a date range that spans across a month/year boundary.
- Test searching for actions performed by a deactivated user.
- Test pagination by ensuring the correct data is loaded for each page.
- Test role-based access by attempting to access the page/API with a non-Admin user.

## 9.3.0 Test Data Needs

- A large set of mock audit log data (100,000+ entries) with diverse users, actions, and timestamps.
- Test data must include entries from deactivated users.
- Test data must include a variety of action types.

## 9.4.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A performance testing tool (e.g., k6, JMeter) to load test the search API endpoint.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration test coverage meets the project standard (80%).
- E2E tests for critical search and filter paths are implemented and passing.
- Performance testing confirms API latency is within the defined NFR.
- Security review confirms that access is restricted to System Administrators.
- UI has been reviewed for accessibility and responsiveness.
- Online help documentation for the Audit Trail feature has been created or updated.
- Story has been successfully deployed and verified in the production environment.

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- Requires a developer comfortable with both frontend (React) and backend (NestJS, Elasticsearch) development.
- Availability of the Elasticsearch indexing pipeline for audit logs is a key prerequisite.
- Staging environment must be populated with realistic test data before QA can begin.

## 11.4.0 Release Impact

This is a key feature for the initial release, especially for customers with strict compliance requirements.

