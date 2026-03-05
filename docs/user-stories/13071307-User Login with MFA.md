# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-009 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User Login with MFA |
| As A User Story | As a security-conscious user whose account has Mul... |
| User Persona | Any user (System Administrator, Finance Manager, C... |
| Business Value | Enhances account security by requiring a second fo... |
| Functional Area | User Authentication & Security |
| Story Theme | Secure User Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful Login with Valid MFA Code

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A user with MFA enabled has navigated to the login page and successfully submitted their correct email and password

### 3.1.5 When

The user is presented with the MFA challenge screen and enters the correct, valid 6-digit code from their authenticator app

### 3.1.6 Then

The system validates the code, establishes a fully authenticated session, and redirects the user to their main dashboard.

### 3.1.7 Validation Notes

Verify that a valid JWT is issued and the user is redirected to the correct post-login destination (e.g., /dashboard).

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Login Attempt with Invalid MFA Code

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A user with MFA enabled is on the MFA challenge screen

### 3.2.5 When

The user enters an incorrect or expired 6-digit code

### 3.2.6 Then

The system displays a clear, non-specific error message like 'Invalid authentication code. Please try again.' and the user remains on the MFA challenge screen.

### 3.2.7 Validation Notes

The input field for the MFA code should be cleared after the failed attempt. The user should not be locked out on the first incorrect attempt.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Login Attempt with Too Many Invalid MFA Codes

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A user has made 5 consecutive failed MFA attempts

### 3.3.5 When

The user submits a 6th consecutive incorrect MFA code

### 3.3.6 Then

The system temporarily locks the login attempt for that user for a configurable period (e.g., 15 minutes) and displays a message like 'Too many failed attempts. Please try again in 15 minutes.'

### 3.3.7 Validation Notes

This aligns with rate-limiting requirements in REQ-NFR-003. Verify that subsequent login attempts for this user are blocked until the lockout period expires.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to Bypass MFA Step

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A user with MFA enabled has successfully authenticated with their password but has not yet completed the MFA challenge

### 3.4.5 When

The user attempts to manually navigate to a protected URL (e.g., /dashboard)

### 3.4.6 Then

The system must prevent access and redirect the user back to the MFA challenge screen.

### 3.4.7 Validation Notes

The authentication state must clearly distinguish between 'password-authenticated' and 'fully-authenticated'. No API calls to protected resources should succeed without a fully-authenticated session.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

MFA Challenge Screen UI and Accessibility

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A user is presented with the MFA challenge screen

### 3.5.5 When

The user views the screen

### 3.5.6 Then

The screen must display a clear instruction (e.g., 'Enter the 6-digit code from your authenticator app'), a single input field optimized for 6 digits, and a 'Verify' button. The page must be fully responsive and meet WCAG 2.1 AA standards.

### 3.5.7 Validation Notes

Test on multiple screen sizes. Verify keyboard navigation and screen reader compatibility as per REQ-INT-001.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated page/view for the MFA challenge.
- A text input field, configured to accept 6 numeric characters.
- A primary action button labeled 'Verify' or 'Submit'.
- An area for displaying error messages.

## 4.2.0 User Interactions

- User enters a 6-digit code into the input field.
- User clicks the 'Verify' button to submit the code.
- The system provides immediate feedback on success (redirect) or failure (error message).

## 4.3.0 Display Requirements

- Clear instructional text explaining what the user needs to do.
- Error messages must be user-friendly and not reveal specific security details.

## 4.4.0 Accessibility Needs

- The input field must have a proper `aria-label`.
- Error messages must be associated with the input field using `aria-describedby`.
- All interactive elements must be keyboard-navigable and have clear focus states.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user account with MFA enabled cannot achieve a fully authenticated state without successfully passing both password and MFA challenges.

### 5.1.3 Enforcement Point

API Gateway and backend service authorization middleware.

### 5.1.4 Violation Handling

The request is rejected with a 401 Unauthorized or 403 Forbidden status, and the user is redirected to the appropriate authentication step.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

More than 5 consecutive failed MFA attempts for an account will trigger a temporary lockout.

### 5.2.3 Enforcement Point

Authentication service (AWS Cognito).

### 5.2.4 Violation Handling

The account's login capability is temporarily suspended. An informative message is displayed to the user.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

This story implements the second step of the login process, which can only be reached after the primary credential validation (email/password) from US-006 is successful.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-008

#### 6.1.2.2 Dependency Reason

The ability for a user to log in with MFA is dependent on the functionality to first set up and enable MFA on their account, which is covered in US-008.

## 6.2.0.0 Technical Dependencies

- AWS Cognito User Pool must be configured to support and enforce MFA.
- Frontend state management library (Zustand) must be able to handle a multi-step authentication flow.

## 6.3.0.0 Data Dependencies

- Requires user records in Cognito to have MFA enabled and a secret key associated with their account.

## 6.4.0.0 External Dependencies

- Relies on the AWS Cognito service for TOTP validation.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The server-side validation of the MFA code must complete within the p95 API response time of <250ms (REQ-NFR-001).

## 7.2.0.0 Security

- All communication must be over HTTPS/TLS 1.2+ (REQ-INT-003).
- The system must be protected against brute-force attacks via rate limiting and temporary lockouts (REQ-NFR-003).
- The session token should only be upgraded to 'fully-authenticated' after successful MFA validation.

## 7.3.0.0 Usability

- The process should be simple and intuitive, with clear instructions and error messages.

## 7.4.0.0 Accessibility

- The MFA challenge screen must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The feature must work correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Integrating with the specific AWS Cognito API flow for responding to an `SOFTWARE_TOKEN_MFA` challenge.
- Managing the intermediate authentication state on the frontend between password success and MFA success.
- Implementing robust error handling for various failure scenarios (invalid code, expired session, account lockout).

## 8.3.0.0 Technical Risks

- Incorrect frontend state management could potentially allow the MFA step to be bypassed.
- Misconfiguration of Cognito's MFA settings could weaken the security posture.

## 8.4.0.0 Integration Points

- AWS Cognito API for verifying the TOTP.
- Frontend application's global state and routing logic.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- End-to-end login flow for an MFA-enabled user.
- Submitting an incorrect code and verifying the error message.
- Triggering the account lockout mechanism after 6 failed attempts.
- Attempting to access a protected route before completing the MFA step.
- Verifying the UI is responsive and accessible via keyboard and screen reader.

## 9.3.0.0 Test Data Needs

- At least two test user accounts in the staging Cognito User Pool with MFA enabled.
- An authenticator app (e.g., Google Authenticator, Authy) configured for the test accounts.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with >80% code coverage
- E2E tests for the MFA login flow are written and passing
- User interface reviewed for responsiveness and adherence to design specifications
- Performance requirements verified under simulated load
- Security review completed to ensure no bypass vulnerabilities exist
- Accessibility audit (automated and manual) passed
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical security feature and a blocker for onboarding System Admins and Finance Managers.
- Requires coordination between frontend and backend developers to manage the auth state.
- The team needs access to the staging AWS Cognito configuration to set up test users.

## 11.4.0.0 Release Impact

Enables mandatory security controls for high-privilege users, a key requirement for the initial pilot (Phase 1) and SOC 2 compliance.

