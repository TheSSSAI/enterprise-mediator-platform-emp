# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-044 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Vendor Views Project Brief via Secure Portal |
| As A User Story | As a Vendor Contact who has been invited to a proj... |
| User Persona | Vendor Contact (External User) |
| Business Value | Enables vendors to evaluate project opportunities,... |
| Functional Area | Proposal and Project Workflow |
| Story Theme | Vendor Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful viewing of Project Brief with a valid link

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A Vendor Contact has received an email with a unique, valid, and non-expired secure link for an active project brief

### 3.1.5 When

The Vendor Contact clicks the link

### 3.1.6 Then

The system validates the token and directs the user to a secure portal page, which displays the complete sanitized Project Brief including Project Title, Scope Summary, Key Deliverables, Required Skills, and Timeline, along with the proposal submission deadline and CTAs for 'Submit Proposal' and 'Ask a Question'.

### 3.1.7 Validation Notes

Verify that all expected sections of the brief are present and that no PII or client-identifying data is visible. The URL should contain a unique token.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempting to access the portal with an invalid or malformed token

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A user attempts to access the project brief portal

### 3.2.5 When

The URL token is invalid, tampered with, or does not exist

### 3.2.6 Then

The system presents a user-friendly '404 Not Found' or 'Invalid Link' error page with guidance to contact support.

### 3.2.7 Validation Notes

Manually alter a valid token in the URL and confirm the correct error page is displayed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to access the portal with an expired link

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

A Vendor Contact has a secure link for a project brief

### 3.3.5 When

The link's token has expired (e.g., the proposal deadline has passed)

### 3.3.6 Then

The system presents a page indicating 'This project opportunity is no longer available' or 'The proposal deadline has passed'. The project brief details are not shown.

### 3.3.7 Validation Notes

Use a test utility to generate an expired token or wait for a real one to expire, then attempt access.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempting to access the portal for a cancelled or on-hold project

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

A Vendor Contact has a valid link, but an Admin has since changed the project status to 'Cancelled' or 'On Hold'

### 3.4.5 When

The Vendor Contact clicks the link

### 3.4.6 Then

The system presents a page indicating the project's current status (e.g., 'This project has been cancelled') and that proposals are no longer being accepted.

### 3.4.7 Validation Notes

As an Admin, send a brief invitation, then change the project status to 'Cancelled'. Then, as the vendor, click the link and verify the correct status page is shown.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Re-accessing the portal after submitting a proposal

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

A Vendor Contact has already submitted a proposal for the project

### 3.5.5 When

The Vendor Contact clicks the original invitation link again

### 3.5.6 Then

The system directs them to a page confirming their submission and showing its current status (e.g., 'Submitted'). The 'Submit Proposal' CTA is disabled or replaced with a 'View Submission' link.

### 3.5.7 Validation Notes

Complete the submission flow (US-049), then navigate back to the original link from the invitation email.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Page Title (Project Name)
- Formatted sections for Scope, Deliverables, Skills, Timeline
- Clearly displayed 'Proposal Submission Deadline'
- Primary Button: 'Submit Proposal'
- Secondary Link/Button: 'Ask a Question'

## 4.2.0 User Interactions

- The page is read-only; no data entry is required to view the brief.
- Clicking 'Submit Proposal' navigates the user to the proposal submission form (US-049).
- Clicking 'Ask a Question' opens the Q&A interface (US-045).

## 4.3.0 Display Requirements

- The layout must be clean, professional, and easy to read, using headings and lists to structure the information.
- The page must be fully responsive, providing an optimal viewing experience on desktop, tablet, and mobile devices.

## 4.4.0 Accessibility Needs

- The page must adhere to WCAG 2.1 Level AA standards, including proper heading structures, ARIA attributes for interactive elements, and sufficient color contrast.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only the sanitized version of the Project Brief can be displayed to external vendors.

### 5.1.3 Enforcement Point

API endpoint serving the project brief data.

### 5.1.4 Violation Handling

The system must ensure the data query explicitly fetches from the sanitized data store, preventing any accidental leakage of PII or original SOW content.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The secure link for viewing a project brief must have a defined expiration, tied to the proposal submission deadline.

### 5.2.3 Enforcement Point

Token validation middleware in the backend.

### 5.2.4 Violation Handling

Access is denied, and an 'expired' message is displayed to the user.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-036

#### 6.1.1.2 Dependency Reason

A Project Brief must be finalized and approved by an Admin before it can be made available for viewing.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

The action of distributing the brief to vendors is what generates the secure link used in this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-043

#### 6.1.3.2 Dependency Reason

This story provides the email notification that contains the secure link, which is the entry point for this story's functionality.

## 6.2.0.0 Technical Dependencies

- A service for generating and validating secure, time-limited, single-purpose tokens (e.g., JWTs).
- A backend API endpoint in the 'Project Service' to fetch sanitized brief data based on a valid token.
- A frontend routing mechanism to handle the tokenized URLs.

## 6.3.0.0 Data Dependencies

- Access to the 'Projects' and 'SOWs' data stores to retrieve the approved, sanitized brief content.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 95th percentile (p95) for the API call fetching the brief data shall be less than 250ms (REQ-NFR-001).
- The Largest Contentful Paint (LCP) for the project brief page shall be under 2.5 seconds (REQ-NFR-001).

## 7.2.0.0 Security

- All communication must be over HTTPS using TLS 1.2 or higher (REQ-INT-003).
- The access token in the URL must be cryptographically secure, non-sequential, and resistant to tampering.
- The endpoint must be protected against enumeration attacks.

## 7.3.0.0 Usability

- The information architecture of the brief must be clear and logical, allowing vendors to quickly assess the project's fit.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The page must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementation of a secure token generation and validation mechanism.
- Requires both frontend (React page) and backend (API endpoint) development.
- Handling multiple states and edge cases (e.g., expired, invalid, cancelled) adds complexity to both frontend and backend logic.

## 8.3.0.0 Technical Risks

- A flaw in the token validation logic could lead to a security vulnerability, allowing unauthorized access to project data.

## 8.4.0.0 Integration Points

- Project Service: To fetch the brief data.
- Authentication Service: To generate and validate the secure token.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a valid link displays the correct, complete, and sanitized brief.
- Test invalid, expired, and tampered-with links to ensure proper error handling.
- Test links for projects in non-active states (Cancelled, On Hold).
- Verify the page is fully responsive across target device viewports.
- Perform security testing on the token validation endpoint.

## 9.3.0.0 Test Data Needs

- An active project with an approved brief.
- A project with a past proposal deadline.
- A project that has been cancelled after invitations were sent.
- A set of valid, invalid, and expired tokens.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Web security scanner (e.g., OWASP ZAP) for vulnerability testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage and passing
- E2E tests for all key scenarios implemented and passing
- User interface reviewed and approved by UX/Product Owner
- Performance requirements verified under simulated load
- Security review of the token mechanism completed and any findings addressed
- Accessibility audit passed against WCAG 2.1 AA
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical enabler for the entire proposal submission workflow.
- It blocks the implementation of US-045 (Ask a Question) and US-049 (Submit Proposal).

## 11.4.0.0 Release Impact

Core functionality required for the initial release to vendors.

