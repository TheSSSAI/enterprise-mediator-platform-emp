# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-053 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Flags or Scores Proposals for Internal Evalu... |
| As A User Story | As a System Administrator evaluating multiple vend... |
| User Persona | System Administrator responsible for managing the ... |
| Business Value | Improves the efficiency and consistency of the pro... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal Evaluation and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Admin applies a numerical score to a proposal for the first time

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin is on the 'Proposal Comparison View' for a project with at least one submitted proposal

### 3.1.5 When

The admin clicks the 4th star of the 5-star rating component for a specific proposal

### 3.1.6 Then

The system saves a score of 4 for that proposal, and the UI updates to show 4 filled stars.

### 3.1.7 Validation Notes

Verify the `internal_score` field for the proposal record in the database is updated to 4. The UI change should be immediate. An audit log entry for the assessment change should be created.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Admin applies a predefined flag to a proposal

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A System Admin is on the 'Proposal Comparison View'

### 3.2.5 When

The admin clicks the 'Add Flag' icon for a proposal and selects 'Top Contender' from the dropdown list

### 3.2.6 Then

The system saves the 'Top Contender' flag for that proposal, and a visually distinct tag (e.g., green with text) appears next to the proposal.

### 3.2.7 Validation Notes

Verify the `internal_flag` field for the proposal record is updated. The dropdown should contain 'Top Contender', 'Good Alternative', 'Needs Review', 'Red Flag'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Admin changes an existing score

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

A proposal has an existing score of 4 stars

### 3.3.5 When

The admin clicks the 2nd star of the same rating component

### 3.3.6 Then

The system updates the score to 2, and the UI updates to show 2 filled stars.

### 3.3.7 Validation Notes

Verify the database record is updated from 4 to 2. The previous value should be captured in the audit log.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Admin removes an existing flag

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

A proposal has an existing flag of 'Top Contender'

### 3.4.5 When

The admin clicks the flag and selects a 'Clear Flag' or 'None' option

### 3.4.6 Then

The system removes the flag from the proposal, and the flag tag disappears from the UI.

### 3.4.7 Validation Notes

Verify the `internal_flag` field in the database is set to NULL or an empty value.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Assessment data persists on page reload

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

An admin has applied a score of 5 and a flag of 'Red Flag' to a proposal

### 3.5.5 When

The admin reloads the 'Proposal Comparison View' page

### 3.5.6 Then

The proposal still displays the 5-star rating and the 'Red Flag' tag.

### 3.5.7 Validation Notes

This confirms the data is being saved to and retrieved from the backend correctly.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

System fails to save an assessment due to a network error

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A System Admin is on the 'Proposal Comparison View'

### 3.6.5 When

The admin applies a score, but the API call to the backend fails

### 3.6.6 Then

The UI reverts the scoring component to its previous state and displays a non-blocking error notification (e.g., a toast) stating 'Failed to save assessment. Please try again.'

### 3.6.7 Validation Notes

Use browser developer tools to simulate a network failure for the API endpoint and verify the UI response.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Assessments are not visible to vendors

### 3.7.3 Scenario Type

Security

### 3.7.4 Given

A System Admin has scored and flagged a proposal submitted by a vendor

### 3.7.5 When

The Vendor Contact associated with that proposal logs in and views their project information

### 3.7.6 Then

There is no indication of the internal score or flag anywhere in the vendor's UI.

### 3.7.7 Validation Notes

Verify that the API endpoints that return proposal data to vendors do not include the `internal_score` or `internal_flag` fields.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 5-star interactive rating component for each proposal.
- An icon or button to trigger a dropdown menu for selecting a flag.
- A colored tag component to display the selected flag (e.g., Green for 'Top Contender', Blue for 'Good Alternative', Yellow for 'Needs Review', Red for 'Red Flag').

## 4.2.0 User Interactions

- Clicking on a star sets the rating.
- Clicking on the flag icon opens a dropdown menu.
- Selecting an option from the flag menu applies the flag.
- Changes are saved automatically via an API call without a separate 'Save' button.

## 4.3.0 Display Requirements

- The scoring and flagging controls must be displayed for each proposal on the 'Proposal Comparison View' (US-052).
- The applied score and flag must be clearly visible and associated with the correct proposal.

## 4.4.0 Accessibility Needs

- The star rating component must be keyboard accessible (e.g., using arrow keys to change the rating).
- The flag dropdown must be fully keyboard navigable.
- The selected flag and score should be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Proposal scores and flags are for internal use only and must never be exposed to Client or Vendor roles.

### 5.1.3 Enforcement Point

API Gateway and Backend Services (Data Serialization Layer).

### 5.1.4 Violation Handling

The sensitive fields (`internal_score`, `internal_flag`) are omitted from API responses sent to non-internal users.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only internal users with project access (System Administrator, Finance Manager) can add, edit, or remove scores and flags.

### 5.2.3 Enforcement Point

API endpoint authorization middleware.

### 5.2.4 Violation Handling

The API will return a 403 Forbidden status code for unauthorized attempts.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-052

#### 6.1.1.2 Dependency Reason

This story adds the flagging and scoring UI elements directly onto the 'Proposal Comparison View' which is created by US-052.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-049

#### 6.1.2.2 Dependency Reason

Requires the existence of submitted proposals in the system to apply scores or flags to.

## 6.2.0.0 Technical Dependencies

- The `Proposal` entity in the database schema must be extended with `internal_score` (INTEGER) and `internal_flag` (VARCHAR or ENUM) columns.
- A backend API endpoint (e.g., `PATCH /api/v1/proposals/{id}/assessment`) must be created to handle updates.

## 6.3.0.0 Data Dependencies

- Requires existing project and proposal data to be present for testing.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to save a score or flag must respond in under 300ms (p95).
- Applying a score or flag should not cause any noticeable performance degradation on the proposal comparison page, even with 20+ proposals displayed.

## 7.2.0.0 Security

- The API endpoint for updating assessments must be protected and only accessible to authenticated internal users.
- The system must prevent unauthorized users from viewing or modifying internal assessment data.

## 7.3.0.0 Usability

- The action of scoring or flagging should be intuitive and require minimal clicks.
- The system should provide clear, immediate visual feedback that the change has been successfully saved.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires a minor database schema change.
- Requires one new, straightforward backend API endpoint.
- Frontend work involves adding existing or simple new components to an established view.
- No complex business logic is involved.

## 8.3.0.0 Technical Risks

- Potential for race conditions if two admins try to score the same proposal simultaneously. The last write will win, which is an acceptable risk for this feature.

## 8.4.0.0 Integration Points

- Integrates with the Audit Log service to record assessment changes.
- Modifies the `Proposal` entity and its corresponding service/repository.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify an admin can add, update, and remove a score.
- Verify an admin can add, update, and remove a flag.
- Verify changes persist after a page refresh.
- Verify error handling for API failures.
- Verify a user with a 'Vendor' role cannot see or modify the assessment data via the UI or API.

## 9.3.0.0 Test Data Needs

- A test project with multiple submitted proposals from different vendors.
- User accounts for a System Admin and a Vendor Contact.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage for new code
- E2E tests for critical paths created and passing
- User interface reviewed and approved by Product Owner/UX designer
- Security requirements validated (role-based access control on API)
- Accessibility requirements (WCAG 2.1 AA) validated
- API documentation (OpenAPI) updated for the new endpoint
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story should be planned in a sprint following the completion of US-052.
- The backend and frontend work can be done in parallel.

## 11.4.0.0 Release Impact

Enhances the core proposal evaluation workflow. It is a key quality-of-life improvement for admins but not a blocker for the initial release of the proposal workflow.

