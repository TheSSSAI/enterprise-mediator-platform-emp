# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-031 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Real-Time SOW Processing Status |
| As A User Story | As a System Administrator, I want to see the real-... |
| User Persona | System Administrator responsible for initiating an... |
| Business Value | Provides critical feedback for a core asynchronous... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Automation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

SOW status is displayed as 'Pending' immediately after successful upload

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A System Admin is on the project workspace page and has just successfully uploaded an SOW document

### 3.1.5 When

The page refreshes or updates after the upload is complete

### 3.1.6 Then

The system shall display a status indicator for the SOW with the text 'Pending'.

### 3.1.7 Validation Notes

Verify in the UI that the status badge appears correctly. Check the database to confirm the SOW record's status is set to 'PENDING'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

SOW status updates to 'Processing' when the AI workflow begins

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An SOW has a status of 'Pending'

### 3.2.5 When

The asynchronous AI Ingestion Service picks up the SOW and begins processing

### 3.2.6 Then

The status indicator in the UI shall automatically update to 'Processing' without requiring a manual page reload.

### 3.2.7 Validation Notes

This can be tested by mocking the backend event. The UI should update via WebSocket, SSE, or a polling mechanism. The status badge should change color and text.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

SOW status updates to 'Processed' upon successful completion

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

An SOW has a status of 'Processing'

### 3.3.5 When

The AI Ingestion Service successfully completes all sanitization and data extraction tasks

### 3.3.6 Then

The status indicator in the UI shall automatically update to 'Processed' (or 'Ready for Review').

### 3.3.7 Validation Notes

Verify the UI updates in near real-time. The 'Processed' status should be visually distinct (e.g., green badge). The user should now be able to proceed to the review step (covered in US-034).

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

SOW status updates to 'Failed' when an error occurs during processing

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

An SOW is being processed by the AI Ingestion Service

### 3.4.5 When

An unrecoverable error occurs during the processing workflow

### 3.4.6 Then

The status indicator in the UI shall automatically update to 'Failed'.

### 3.4.7 Validation Notes

Simulate a processing failure in the backend. Verify the UI updates to show the 'Failed' state (e.g., red badge).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Admin can view details for a failed SOW process

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An SOW has a status of 'Failed'

### 3.5.5 When

The System Admin hovers over or clicks an information icon next to the 'Failed' status

### 3.5.6 Then

The system shall display a tooltip or modal containing an error summary and a correlation ID for support, as per REQ-FUN-002.

### 3.5.7 Validation Notes

Verify that the UI element for showing error details is present and functional for SOWs in the 'Failed' state only.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A status 'badge' or 'pill' component to display the SOW status.
- An information icon (e.g., 'i' in a circle) that appears next to the status badge only when the status is 'Failed'.
- A tooltip or small modal to display failure details.

## 4.2.0 User Interactions

- The status badge should update automatically without user interaction.
- Hovering or clicking the information icon on a 'Failed' status should reveal the error details.

## 4.3.0 Display Requirements

- The status must be clearly visible within the project's SOW management section.
- Each status must have a distinct, theme-compliant color code: Pending (neutral, e.g., grey), Processing (active, e.g., blue with animation), Processed (success, e.g., green), Failed (error, e.g., red).

## 4.4.0 Accessibility Needs

- Color must not be the only means of conveying status. Clear text labels are required.
- The status update region should use `aria-live` attributes so screen readers announce changes.
- All interactive elements (like the info icon) must be keyboard-focusable and have appropriate ARIA labels.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'An SOW can only have one of the following statuses at any given time: PENDING, PROCESSING, PROCESSED, FAILED.', 'enforcement_point': 'Backend (AI Ingestion Service and Database Schema)', 'violation_handling': 'The system should log an error if an invalid state transition is attempted. The database should use an ENUM type for the status column to enforce constraints.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

A project entity must exist to which an SOW can be attached.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-030

#### 6.1.2.2 Dependency Reason

The SOW upload functionality is the trigger for this story. The upload API must be implemented to initiate the process.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint to fetch the current status of an SOW (e.g., GET /api/v1/projects/{id}/sow).
- A real-time communication channel (WebSocket, SSE) or a frontend polling mechanism to get status updates.
- The AI Ingestion Service must be capable of updating the SOW status in the database at each stage of its workflow.

## 6.3.0.0 Data Dependencies

- The `SOW` data entity must include a 'status' field.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- If polling is used, the frequency should be reasonable (e.g., every 5-10 seconds) to avoid excessive server load.
- The status update on the UI should be visually instantaneous once the data is received from the backend.

## 7.2.0.0 Security

- The API endpoint for fetching SOW status must be protected and only accessible to authorized users (System Admins) with access to that specific project.

## 7.3.0.0 Usability

- The status indicator should be intuitive and immediately understandable.
- An animated indicator for the 'Processing' state provides better feedback that the system is actively working.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity is implementing the real-time update mechanism. A WebSocket/SSE solution is more complex but provides a superior user experience aligned with the '2040' aesthetic.
- Ensuring the asynchronous backend service reliably reports its status at every stage requires robust state management and error handling.
- Coordinating frontend and backend for the real-time communication protocol.

## 8.3.0.0 Technical Risks

- If using polling, there's a risk of creating unnecessary load or having slightly delayed status updates.
- If using WebSockets, ensuring the connection is stable and gracefully handled on disconnect/reconnect adds complexity.

## 8.4.0.0 Integration Points

- Frontend Project Workspace UI.
- Backend Project Service (for the status API).
- Backend AI Ingestion Service (for status updates).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify UI component renders correctly for all four statuses (Pending, Processing, Processed, Failed).
- E2E Test: Upload an SOW and mock the backend to transition through all statuses, asserting the UI updates correctly at each step without a page reload.
- E2E Test: Mock a 'Failed' status and verify the error details are displayed correctly on user interaction.
- Integration Test: Verify that when the AI service updates the database, the status API reflects the change immediately.

## 9.3.0.0 Test Data Needs

- Test projects with SOWs in each of the four possible states.

## 9.4.0.0 Testing Tools

- Jest for frontend/backend unit tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >80% coverage and passing
- Integration testing between the status API and the UI is completed successfully
- E2E tests for the status lifecycle (happy path and failure) are implemented and passing
- User interface reviewed and approved by UX/Product for visual correctness and accessibility
- Performance impact of the update mechanism has been assessed
- Security requirements validated
- Documentation for the status API endpoint is created/updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is critical for the usability of the SOW upload feature. It should be prioritized immediately after the basic upload functionality (US-030).
- A decision on the real-time update technology (Polling vs. WebSocket/SSE) must be made during sprint planning as it significantly impacts implementation effort.

## 11.4.0.0 Release Impact

This is a core UX component of the SOW automation workflow. The workflow is incomplete without this visual feedback.

