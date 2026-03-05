# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-008 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin/Finance Manager MFA Enablement |
| As A User Story | As a System Administrator or Finance Manager, I wa... |
| User Persona | System Administrator, Finance Manager |
| Business Value | Enhances security for high-privilege accounts, pro... |
| Functional Area | User Account Management & Security |
| Story Theme | Authentication and Security Hardening |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful MFA setup using QR code

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Admin or Finance Manager on my account's security settings page, and MFA is currently disabled

### 3.1.5 When

I click the 'Enable MFA' button

### 3.1.6 Then

The system presents a multi-step setup interface, displaying a unique QR code and a manual setup key.

### 3.1.7 Validation Notes

Verify the QR code and key are generated via the authentication service (AWS Cognito).

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful verification and activation

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I have started the MFA setup and see the QR code

### 3.2.5 When

I scan the QR code with my authenticator app, enter the generated 6-digit code into the verification field, and click 'Verify & Enable'

### 3.2.6 Then

The system validates the code and displays a success message confirming that MFA is now active.

### 3.2.7 Validation Notes

The user's MFA status in AWS Cognito should be updated to 'ENABLED'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Secure storage of recovery codes

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have just successfully verified my MFA device

### 3.3.5 When

The system confirms MFA activation

### 3.3.6 Then

The system must display a set of single-use recovery codes.

### 3.3.7 Validation Notes

The interface should provide 'Copy Codes' and 'Download Codes (.txt)' buttons and include a strong warning that these codes will not be shown again and must be stored securely.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Entering an incorrect verification code

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the MFA verification step

### 3.4.5 When

I enter an invalid or expired 6-digit code and click 'Verify & Enable'

### 3.4.6 Then

The system displays a clear error message, such as 'Invalid verification code. Please try again.', without revealing why it failed.

### 3.4.7 Validation Notes

The user's MFA status should remain disabled. The system should allow for retries.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempting to enable MFA when it is already enabled

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a logged-in System Admin with MFA already active

### 3.5.5 When

I navigate to my account's security settings page

### 3.5.6 Then

The system displays the status 'MFA is Active' and does not show the 'Enable MFA' button.

### 3.5.7 Validation Notes

The page should instead show options to 'Disable MFA' or 'View Recovery Codes' (these actions may be handled in separate stories).

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Cancelling the MFA setup process

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I have started the MFA setup process and the QR code is displayed

### 3.6.5 When

I click a 'Cancel' button or navigate away from the setup page

### 3.6.6 Then

The MFA setup process is terminated, and my account's MFA status remains disabled.

### 3.6.7 Validation Notes

Verify that no partial MFA configuration is saved for the user.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Security' section in the user's account settings.
- 'Enable MFA' button.
- A modal or dedicated page for the setup flow.
- Display area for QR code image.
- Display area for manual setup key (text).
- Input field for 6-digit verification code.
- 'Verify & Enable' button.
- 'Cancel' button.
- Success and error message banners/toasts.
- Display area for recovery codes with 'Copy' and 'Download' buttons.

## 4.2.0 User Interactions

- User clicks to initiate the MFA setup.
- User can copy the manual key to the clipboard.
- User enters a 6-digit code.
- User receives immediate feedback on successful or failed verification.

## 4.3.0 Display Requirements

- Clear instructions must be provided at each step of the setup process.
- A strong warning must be displayed regarding the importance of saving recovery codes in a safe place.

## 4.4.0 Accessibility Needs

- The entire flow must be navigable using a keyboard.
- The QR code must have a text-based alternative (the manual setup key).
- All form fields, buttons, and instructional text must have appropriate labels and ARIA attributes for screen readers, per WCAG 2.1 AA standards (REQ-INT-001).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

MFA is mandatory for System Administrator and Finance Manager roles.

### 5.1.3 Enforcement Point

This story enables setup. Enforcement (e.g., forcing setup on first login) may be a separate story, but this flow is the mechanism to comply.

### 5.1.4 Violation Handling

N/A for this story, which is about enabling. A separate story would handle forcing non-compliant users into this setup flow.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Recovery codes are single-use and must be securely generated.

### 5.2.3 Enforcement Point

During the final step of MFA activation.

### 5.2.4 Violation Handling

N/A. This is a system design requirement.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

User must be able to log in to access their account settings.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

N/A

#### 6.1.2.2 Dependency Reason

A user story for creating the basic 'Account Settings' page must be completed first.

## 6.2.0.0 Technical Dependencies

- AWS Cognito User Pool must be configured to support TOTP-based MFA (REQ-NFR-003).
- Backend User Service must have endpoints to interface with the Cognito API for associating and verifying software tokens.
- Frontend state management solution (Zustand) to handle the multi-step UI flow.

## 6.3.0.0 Data Dependencies

- Requires access to the authenticated user's profile to update their MFA status.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The QR code and verification response should appear in under 500ms.

## 7.2.0.0 Security

- The MFA secret key must never be stored in client-side logs or insecurely on the server.
- All communication must be over HTTPS (REQ-INT-003).
- The system must follow Cognito's security best practices for MFA implementation.
- Recovery codes must be generated with sufficient entropy and stored securely (hashed) until used.

## 7.3.0.0 Usability

- The setup process should be intuitive and require minimal technical knowledge.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The UI must be responsive and function correctly on all supported browsers (latest two versions of Chrome, Firefox, Safari, Edge) as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend and backend development.
- Integration with AWS Cognito API for a secure, multi-step process.
- Careful state management on the frontend for the setup flow.
- Secure handling and presentation of recovery codes.

## 8.3.0.0 Technical Risks

- Incorrect integration with Cognito could lead to users being unable to enable MFA or being locked out.
- Insecure handling of recovery codes could compromise the security benefit of MFA.

## 8.4.0.0 Integration Points

- AWS Cognito API (specifically `AssociateSoftwareToken` and `VerifySoftwareToken`).
- Backend User Service.
- Frontend Account Settings module.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Complete the happy path flow from start to finish.
- Test entering an incorrect verification code multiple times.
- Test cancelling the flow at different stages.
- Verify the UI state for a user who already has MFA enabled.
- Use an E2E test (Playwright) to automate the entire setup process.
- Verify keyboard-only navigation and screen reader compatibility.

## 9.3.0.0 Test Data Needs

- A test account (System Admin or Finance Manager) without MFA enabled.
- A test account (System Admin or Finance Manager) with MFA already enabled.
- An authenticator app (e.g., Google Authenticator, Authy) for manual testing.

## 9.4.0.0 Testing Tools

- Jest
- Playwright
- Browser accessibility testing tools (e.g., Axe, Lighthouse).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% coverage and all passing
- E2E tests for the happy path and key error conditions are implemented and passing
- User interface reviewed and approved by UX/Product Owner
- Security review completed and any findings addressed
- Accessibility audit passed against WCAG 2.1 AA standards
- User-facing documentation in the help guide (REQ-NFR-008) is created or updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational security feature and a prerequisite for US-009 (User Login with MFA).
- Requires coordination between frontend and backend developers.

## 11.4.0.0 Release Impact

Critical for any release involving privileged user access. A key feature for meeting security and compliance requirements.

