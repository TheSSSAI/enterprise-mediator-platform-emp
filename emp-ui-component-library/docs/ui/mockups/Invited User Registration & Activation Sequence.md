{
  "diagram_info": {
    "diagram_name": "Invited User Registration & Activation Sequence",
    "diagram_type": "sequenceDiagram",
    "purpose": "Technical visualization of the user onboarding process via secure invitation link, detailing the interaction between the user, frontend, backend services, and external identity providers as defined in US-004 and Sequence Design ID 474.",
    "target_audience": [
      "Backend Developers",
      "Frontend Developers",
      "Security Architects",
      "QA Engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5-8 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested for Sequence Diagram strict mode",
  "rendering_notes": "Uses auto-numbering for step clarity and notes for specific security requirements like Argon2id hashing and atomic transactions.",
  "diagram_elements": {
    "actors_systems": [
      "External User",
      "Frontend SPA",
      "Backend API",
      "PostgreSQL DB",
      "AWS Cognito"
    ],
    "key_processes": [
      "Token Validation",
      "Password Complexity Check",
      "Password Hashing (Argon2id)",
      "Account Activation",
      "Identity Provider Confirmation"
    ],
    "decision_points": [
      "Token Validity Check",
      "Password Policy Check",
      "Cognito Integration Success"
    ],
    "success_paths": [
      "User clicks link -> Token Validated -> Form Submitted -> Account Activated -> Redirect to Login"
    ],
    "error_scenarios": [
      "Token Expired (410)",
      "Token Invalid (404)",
      "Token Already Used (409)",
      "Weak Password",
      "Cognito Failure"
    ],
    "edge_cases_covered": [
      "Database rollback on Cognito failure",
      "Re-validation of token on submit"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Sequence diagram showing the flow of a user registering via an invitation link. It moves from the user clicking a link, to the frontend validating the token with the backend, submitting the registration form, and the backend coordinating with the database and AWS Cognito to finalize the account.",
    "color_independence": "Success and failure paths are textually distinguished in notes and alt blocks",
    "screen_reader_friendly": "Sequential ordering logically follows the time-based interaction",
    "print_compatibility": "High contrast lines and text ensure readability in grayscale"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Horizontal scrolling required on mobile due to participant width",
    "theme_compatibility": "Neutral colors used for compatibility with light/dark modes",
    "performance_notes": "Standard rendering complexity"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the 'POST /activate' endpoint and the registration frontend page.",
    "stakeholder_value": {
      "developers": "Defines the exact API contract and transactional boundaries.",
      "designers": "Highlights necessary UI states (Loading, Error, Success).",
      "product_managers": "Verifies the security and compliance steps (Audit logging, MFA setup readiness).",
      "QA_engineers": "Provides a blueprint for integration tests (Token expiry, DB rollback scenarios)."
    },
    "maintenance_notes": "Update if the identity provider changes or if additional onboarding steps (e.g., TOS acceptance) are added.",
    "integration_recommendations": "Link to US-004 and the API Specification for '/api/v1/user-invitations'."
  },
  "validation_checklist": [
    "✅ Security requirement (Argon2id) explicitly noted",
    "✅ Atomic transaction boundaries defined",
    "✅ Edge cases (Expired/Used tokens) handled via Alt blocks",
    "✅ Integration with AWS Cognito included",
    "✅ Audit logging step included as per REQ-FUN-005",
    "✅ Frontend/Backend separation clear",
    "✅ User feedback loops (Success/Error) visualized"
  ]
}

---

# Mermaid Diagram

```mermaid
sequenceDiagram
    autonumber
    actor User as External User
    participant FE as Frontend SPA
    participant API as Backend API
    participant DB as PostgreSQL DB
    participant Auth as AWS Cognito

    Note over User, Auth: Phase 1: Invitation Link Validation
    User->>FE: Clicks Invitation Link<br/>(/register?token=XYZ)
    activate FE
    FE->>API: GET /api/v1/user-invitations/{token}
    activate API
    API->>DB: SELECT * FROM UserInvitations WHERE Token = @token
    activate DB
    DB-->>API: Return Invitation Record
    deactivate DB
    
    alt Token Invalid / Expired / Used
        API-->>FE: 404/410/409 Error
        FE->>User: Display "Link Expired or Invalid" Page
    else Token Valid
        API-->>FE: 200 OK { email, name, role }
        FE->>User: Render Registration Form<br/>(Pre-filled Email, Password Fields)
    end
    deactivate API

    Note over User, Auth: Phase 2: Account Activation & Security Setup
    User->>FE: Enters Name & Strong Password
    FE->>FE: Client-side Validation<br/>(Complexity Match)
    FE->>API: POST /api/v1/user-invitations/{token}/activate
    activate API
    
    API->>API: Re-validate Token & Password Complexity
    
    opt Validation Failure
        API-->>FE: 400 Bad Request
        FE->>User: Show Inline Error
    end

    API->>API: Hash Password (Argon2id)
    
    Note right of API: Start Atomic Transaction
    API->>DB: UPDATE Users SET Status='Active', Pwd=@hash<br/>WHERE Id=@userId
    API->>DB: UPDATE UserInvitations SET UsedAt=NOW()<br/>WHERE Token=@token
    
    API->>Auth: AdminConfirmSignUp (Link User to Cognito)
    activate Auth
    
    alt Cognito Failure
        Auth-->>API: Error (5xx/4xx)
        API->>DB: ROLLBACK Transaction
        API-->>FE: 500 Internal Server Error
        FE->>User: Display "System Error"
    else Cognito Success
        Auth-->>API: 200 OK (Confirmed)
        deactivate Auth
        
        API->>DB: INSERT INTO AuditLog (Action="User Activated")
        API->>DB: COMMIT Transaction
        
        API-->>FE: 200 OK { success: true }
        FE->>User: Redirect to Login Page<br/>with "Registration Successful" Toast
    end
    deactivate API
    deactivate FE
```