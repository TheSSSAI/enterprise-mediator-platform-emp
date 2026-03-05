# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-095 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Client Approves Milestone via Secure Link |
| As A User Story | As a Client Contact, I want to approve or reject a... |
| User Persona | Client Contact: An external user who is associated... |
| Business Value | Accelerates project velocity by removing login bar... |
| Functional Area | Project Lifecycle Management |
| Story Theme | External User Interaction & Workflow Automation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Client successfully approves a milestone

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A project milestone has a status of 'Pending Approval' and an approval request has been sent to the Client Contact

### 3.1.5 When

The Client Contact clicks the 'Approve' button

### 3.1.6 Then

The milestone's status in the database is updated to 'Approved'.

### 3.1.7 And

A notification is sent to the internal System Administrator responsible for the project.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Alternative Flow: Client rejects a milestone with a reason

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

The Client Contact is on the secure milestone approval page

### 3.2.5 When

The Client Contact provides a reason and confirms the rejection

### 3.2.6 Then

The milestone's status is updated to 'Rejected'.

### 3.2.7 And

A notification, including the rejection reason, is sent to the System Administrator.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: Client clicks an expired link

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A milestone approval link was generated and its configured lifespan (e.g., 72 hours) has passed

### 3.3.5 When

The Client Contact clicks the expired link

### 3.3.6 Then

The system displays a page indicating the link has expired.

### 3.3.7 And

No project information is displayed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Client clicks a previously used link

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A milestone approval link has already been used to either approve or reject the milestone

### 3.4.5 When

The Client Contact clicks the same link again

### 3.4.6 Then

The system displays a page indicating the final status of the milestone (e.g., 'This milestone was approved on YYYY-MM-DD').

### 3.4.7 And

No further actions can be taken from this page.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Security: User attempts to access with an invalid or tampered token

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A user has a URL with a malformed, non-existent, or tampered-with token

### 3.5.5 When

The user attempts to navigate to the URL

### 3.5.6 Then

The system returns a generic '404 Not Found' or '403 Forbidden' error page.

### 3.5.7 And

No sensitive application or project data is exposed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Dedicated, unauthenticated web page for milestone review
- Clear display of Project Name and Milestone details (description, deliverables)
- Prominent 'Approve' button
- Prominent 'Reject' button
- Modal or text area for capturing rejection reason
- Success/Confirmation message display area
- Error message display area (for expired/used links)

## 4.2.0 User Interactions

- Single-click approval.
- Two-step rejection (click reject, enter reason, confirm).
- The page should be self-contained with no navigation to other parts of the application.

## 4.3.0 Display Requirements

- The page must be lightweight and load quickly.
- The design should be professional and consistent with the EMP brand aesthetic.
- The page must be fully responsive for desktop, tablet, and mobile browsers.

## 4.4.0 Accessibility Needs

- Adherence to WCAG 2.1 Level AA standards.
- All interactive elements (buttons, text fields) must be keyboard-navigable and have clear focus states.
- All elements must have appropriate ARIA labels for screen reader compatibility.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Milestone approval tokens must have a configurable, finite lifespan (e.g., 72 hours).

### 5.1.3 Enforcement Point

API endpoint that validates the token upon page load.

### 5.1.4 Violation Handling

Display the 'Link Expired' error page.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A milestone approval token must be single-use. Once an action (Approve/Reject) is taken, the token is permanently invalidated.

### 5.2.3 Enforcement Point

API endpoint that processes the approval/rejection action.

### 5.2.4 Violation Handling

Display the 'Link Already Used' page on subsequent attempts.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A reason for rejection is mandatory.

### 5.3.3 Enforcement Point

Frontend UI and Backend API validation.

### 5.3.4 Violation Handling

Frontend displays a validation error. API returns a 400 Bad Request if the reason is missing.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

A project must be created to have milestones.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-002

#### 6.1.2.2 Dependency Reason

A Client Contact must be created and associated with a client to receive the approval request.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

REQ-FUN-003 (Implied Story)

#### 6.1.3.2 Dependency Reason

Functionality to create and manage project milestones within a project must exist.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

REQ-FUN-005 (Implied Story)

#### 6.1.4.2 Dependency Reason

The notification service must be capable of sending templated emails via AWS SES.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

REQ-FUN-005 (Implied Story)

#### 6.1.5.2 Dependency Reason

The audit trail service must be available to log the approval/rejection event.

## 6.2.0.0 Technical Dependencies

- Secure token generation service (e.g., using JWT with short expiry).
- AWS Simple Email Service (SES) integration for sending emails.
- A dedicated, unauthenticated route/page in the frontend application.

## 6.3.0.0 Data Dependencies

- Requires access to Project, Milestone, and User (Client Contact) data models and their relationships.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The secure approval page should have a Largest Contentful Paint (LCP) of under 2.0 seconds.
- The backend API response time for processing the approval/rejection should be under 200ms (p95).

## 7.2.0.0 Security

- Tokens must be generated using a cryptographically secure random string or a signed JWT.
- All communication must be over HTTPS (TLS 1.2+).
- The token validation endpoint must be protected against enumeration or brute-force attacks via rate limiting.
- The token must be invalidated immediately after use to prevent replay attacks.

## 7.3.0.0 Usability

- The entire process, from email to final confirmation, should be intuitive and require no prior training for the Client Contact.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The page must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge on both desktop and mobile.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a secure, single-use, time-limited token mechanism is critical and non-trivial.
- Requires creating a separate, unauthenticated user flow and UI, which can be complex in a single-page application (SPA) primarily designed for authenticated users.
- Ensuring the state change (milestone status, token invalidation) is an atomic operation in the backend.

## 8.3.0.0 Technical Risks

- An insecure token implementation could allow unauthorized milestone approvals.
- Potential for race conditions if a user double-clicks the approval button rapidly; the backend must handle this idempotently.

## 8.4.0.0 Integration Points

- Project Service: To update milestone status.
- Notification Service: To send email and internal notifications.
- User Service: To identify the Client Contact.
- Audit Service: To log the event.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Full E2E flow: Trigger request, intercept email, click link, approve milestone, verify DB state and audit log.
- E2E flow for rejection, including reason.
- Attempting to use an expired token.
- Attempting to reuse an invalidated token.
- Submitting a rejection without a reason.
- Keyboard-only navigation and interaction on the approval page.
- Screen reader validation of the approval page.

## 9.3.0.0 Test Data Needs

- A project with at least one milestone in a 'Pending Approval' state.
- An active Client Contact user associated with the project's client.
- Configured email templates for the approval request.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- An email-catching tool (e.g., MailHog, Ethereal) for the E2E test environment.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% code coverage for new logic
- E2E tests for happy path and key error conditions are implemented and passing
- User interface reviewed and approved by a UX/UI designer
- Security review of the token mechanism completed and any findings addressed
- Accessibility audit (automated and manual) passed
- All related documentation (e.g., API specs) has been updated
- Story has been deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical feature for the core project workflow and enables financial processes. It should be prioritized after the basic project and milestone management stories are complete.
- Requires coordination between backend (API, token logic) and frontend (unauthenticated page) development.

## 11.4.0.0 Release Impact

- This feature is a key differentiator for client experience and is necessary for the end-to-end project lifecycle to function smoothly. It is a required component for the initial product launch.

