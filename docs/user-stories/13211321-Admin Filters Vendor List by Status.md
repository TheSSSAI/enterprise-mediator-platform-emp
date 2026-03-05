# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-023 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Filters Vendor List by Status |
| As A User Story | As a System Administrator, I want to filter the ve... |
| User Persona | System Administrator. This user is responsible for... |
| Business Value | Improves operational efficiency by allowing admins... |
| Functional Area | Entity Management - Vendor |
| Story Theme | Vendor Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Filter by a single status

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The System Admin is on the Vendor List page viewing a list of vendors with various statuses

### 3.1.5 When

the Admin selects the 'Active' status from the filter control

### 3.1.6 Then

the vendor list immediately updates to display only vendors with the 'Active' status, and the total count of vendors shown is updated.

### 3.1.7 Validation Notes

Verify the API call includes `?status=Active`. Check that every vendor displayed in the list has the 'Active' status. The filter control should visually indicate that 'Active' is the selected filter.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Filter by multiple statuses

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The System Admin is on the Vendor List page

### 3.2.5 When

the Admin selects both 'Active' and 'Pending Vetting' statuses from the filter control

### 3.2.6 Then

the vendor list updates to display all vendors whose status is either 'Active' OR 'Pending Vetting'.

### 3.2.7 Validation Notes

Verify the API call includes query parameters for both statuses (e.g., `?status=Active&status=Pending%20Vetting`). Check the displayed list to confirm it contains a mix of vendors with only these two statuses.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Clear an active filter

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The System Admin is viewing a filtered list of vendors on the Vendor List page

### 3.3.5 When

the Admin clears the status filter

### 3.3.6 Then

the filter is removed, and the vendor list reverts to displaying all vendors regardless of their status.

### 3.3.7 Validation Notes

The filter control should return to its default state. The API call should be made without any status query parameters. The list should show vendors with 'Active', 'Pending Vetting', and 'Deactivated' statuses.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Filter results in no matching vendors

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The System Admin is on the Vendor List page, and there are no vendors with the 'Deactivated' status

### 3.4.5 When

the Admin applies the 'Deactivated' status filter

### 3.4.6 Then

the list area displays a user-friendly message, such as 'No vendors match the selected criteria', instead of an empty table.

### 3.4.7 Validation Notes

Ensure the message is clear and centered within the list container. The pagination controls should be hidden or disabled.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Filter interacts correctly with search functionality

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

The System Admin has searched for vendors with the term 'Innovate' on the Vendor List page

### 3.5.5 When

the Admin then applies the 'Active' status filter

### 3.5.6 Then

the list displays only vendors that contain the term 'Innovate' in their name AND have the 'Active' status.

### 3.5.7 Validation Notes

The resulting API call should contain parameters for both search and filter (e.g., `?search=Innovate&status=Active`). The logic should be a logical AND.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Filter interacts correctly with pagination

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

The System Admin is viewing page 3 of a paginated vendor list

### 3.6.5 When

the Admin applies any status filter

### 3.6.6 Then

the view automatically resets to page 1 of the newly filtered results, and the pagination controls update to reflect the new total number of filtered vendors.

### 3.6.7 Validation Notes

Test by navigating to a page other than the first, applying a filter, and verifying the page number resets to 1.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A multi-select dropdown or checkbox group labeled 'Status' with options: 'Pending Vetting', 'Active', 'Deactivated'.
- A 'Clear' or 'Reset' button/icon to remove all active status filters.
- A message area to display when no results are found.

## 4.2.0 User Interactions

- Selecting/deselecting a status should trigger an update to the vendor list without a full page reload.
- The state of the filter (which options are selected) must be clearly and visually represented in the UI.
- The filter control must be fully usable with both mouse and keyboard.

## 4.3.0 Display Requirements

- The URL should update with query parameters to reflect the applied filter (e.g., `/vendors?status=Active`), making the filtered view shareable and bookmarkable.

## 4.4.0 Accessibility Needs

- The filter control must be WCAG 2.1 AA compliant, including proper labels (`aria-label`), keyboard navigation, and screen reader compatibility, as per REQ-INT-001.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The available filter options must correspond directly to the defined Vendor statuses in the system.', 'enforcement_point': 'Frontend UI component and Backend API validation.', 'violation_handling': 'If an invalid status is passed to the API, a 400 Bad Request error should be returned.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-021

#### 6.1.1.2 Dependency Reason

This story adds filtering functionality to the vendor list view, which must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-022

#### 6.1.2.2 Dependency Reason

The interaction between filtering and searching needs to be considered. The search functionality should be implemented or its design finalized to ensure compatibility.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for fetching vendors (`/api/v1/vendors`) must be able to accept one or more status query parameters.
- Frontend state management solution (Zustand) to handle filter state.
- UI component library (Radix UI) for the multi-select dropdown.

## 6.3.0.0 Data Dependencies

- The `Vendor` data entity must have a `status` attribute with a defined set of possible values ('Pending Vetting', 'Active', 'Deactivated').

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Applying or clearing a filter must update the vendor list in under 500ms for a dataset of up to 10,000 vendors, in line with REQ-NFR-001.
- The backend database query must be optimized with an index on the `status` column of the `vendors` table.

## 7.2.0.0 Security

- The API endpoint for fetching filtered vendors must be protected by the system's RBAC model, accessible only to authorized roles like System Administrator, as per REQ-SEC-001.

## 7.3.0.0 Usability

- The filter's purpose and current state should be immediately obvious to the user.
- The interaction should feel instantaneous to the user.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards as defined in REQ-INT-001.

## 7.5.0.0 Compatibility

- Functionality must be consistent across the latest two major versions of Chrome, Firefox, Safari, and Edge, as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires coordinated changes in both frontend and backend.
- Frontend state management for the filter, search, and pagination parameters needs to be robust.
- Ensuring the URL reflects the state adds a minor layer of complexity.

## 8.3.0.0 Technical Risks

- Potential for performance degradation on the backend if the database query is not properly indexed and the vendor list grows very large.

## 8.4.0.0 Integration Points

- Frontend: Integrates with the main Vendor List component.
- Backend: Modifies the existing `getVendors` service and controller.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Test filtering with each status individually.
- Test filtering with every combination of two statuses.
- Test filtering with all three statuses selected (should be equivalent to no filter).
- Test clearing the filter from a single and multi-selected state.
- Test the filter/search interaction by applying filter first, then search, and vice-versa.
- Test the filter/pagination interaction.

## 9.3.0.0 Test Data Needs

- A set of test vendors with a distribution of all three statuses ('Pending Vetting', 'Active', 'Deactivated').
- At least one status that has no vendors assigned to it to test the 'no results' case.
- Enough vendors to trigger pagination (e.g., >25 if the page size is 25).

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Axe for accessibility testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria are met and have been validated by QA.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are written and achieve >80% code coverage for the new logic.
- E2E tests covering the primary scenarios are implemented and passing.
- Accessibility checks (automated and manual) have been performed and passed.
- Performance of the filter operation meets the specified NFR.
- The feature is deployed and verified on the staging environment.
- Any necessary user or technical documentation has been updated.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational usability feature for a core admin view. It should be prioritized early in the development of the Vendor Management module, immediately after the basic list view is complete.

## 11.4.0.0 Release Impact

- Improves the core user experience for System Administrators. Its absence would be a noticeable usability gap.

