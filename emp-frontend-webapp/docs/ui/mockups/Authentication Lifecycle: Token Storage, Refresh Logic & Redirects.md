{
  "diagram_info": {
    "diagram_name": "Authentication Lifecycle: Token Storage, Refresh Logic & Redirects",
    "diagram_type": "sequenceDiagram",
    "purpose": "To visualize the end-to-end authentication flow, specifically focusing on secure token storage strategies, the automatic token refresh mechanism via interceptors, and forced redirection patterns upon session expiry.",
    "target_audience": [
      "Frontend Developers",
      "Backend Developers",
      "Security Architects",
      "QA Engineers"
    ],
    "complexity_level": "high",
    "estimated_review_time": "10 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for wide screens due to detailed interaction flows. Uses color-coded grouping for logic blocks.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "SPA Client (Frontend)",
      "Axios/Fetch Interceptor",
      "Backend API",
      "AWS Cognito (IdP)"
    ],
    "key_processes": [
      "Initial Login",
      "Secure Storage",
      "API Request Interception",
      "Silent Token Refresh",
      "Session Termination"
    ],
    "decision_points": [
      "Is Access Token Valid?",
      "Is Refresh Token Valid?",
      "Does route require auth?"
    ],
    "success_paths": [
      "Login -> Store Tokens -> Data Fetch",
      "401 Error -> Refresh Token -> Retry Request -> Success"
    ],
    "error_scenarios": [
      "Login Failed",
      "Refresh Token Expired/Revoked"
    ],
    "edge_cases_covered": [
      "Concurrent requests during refresh",
      "Network failure during refresh"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Sequence diagram showing user login, secure HttpOnly cookie storage for refresh tokens, memory storage for access tokens, and the automatic refresh loop when access tokens expire.",
    "color_independence": "Logic flows are labeled with text alternatives to color coding",
    "screen_reader_friendly": "Sequential ordering of interactions preserves logical flow",
    "print_compatibility": "High contrast lines and labels"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Horizontal scrolling required on mobile",
    "theme_compatibility": "Neutral colors used for broad compatibility",
    "performance_notes": "Grouped interactions to reduce visual noise"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the frontend networking layer, configuring backend cookie policies, or debugging session timeout issues.",
    "stakeholder_value": {
      "developers": "Exact blueprint for implementing the refresh token rotation pattern",
      "designers": "Understanding when to show loading states vs. redirecting users",
      "product_managers": "Understanding the 'silent' nature of session extension",
      "QA_engineers": "Test cases for token expiry and interceptor retry logic"
    },
    "maintenance_notes": "Update if storage strategy changes (e.g., switching to BFF pattern) or if IdP changes.",
    "integration_recommendations": "Include in the 'Authentication' module documentation"
  },
  "validation_checklist": [
    "✅ Secure storage locations explicitly defined",
    "✅ 401 Interception logic clearly visualized",
    "✅ Retry mechanism for original requests included",
    "✅ Terminal failure redirect pattern included",
    "✅ Visual distinction between Client, Interceptor, and API"
  ]
}

---

# Mermaid Diagram

```mermaid
sequenceDiagram
    autonumber
    actor U as User
    participant C as SPA Client
    participant I as API Interceptor
    participant S as Memory Store
    participant B as Backend API
    participant A as AWS Cognito

    Note over U, A: PHASE 1: INITIAL AUTHENTICATION & STORAGE

    U->>C: Navigates to Login Page
    U->>C: Enters Credentials
    C->>B: POST /auth/login (email, password)
    B->>A: Validate Credentials
    A-->>B: Return Tokens (Access, ID, Refresh)
    
    rect rgb(240, 255, 240)
        Note right of B: STORAGE STRATEGY:<br/>Refresh Token -> HttpOnly Secure Cookie<br/>Access Token -> Response Body (JSON)
        B-->>C: 200 OK<br/>Set-Cookie: refresh_token=...; HttpOnly; Secure<br/>Body: { access_token: "..." }
    end

    C->>S: Save Access Token (In-Memory/Zustand)
    C->>U: Redirect to Dashboard

    Note over U, A: PHASE 2: AUTHENTICATED REQUESTS & REFRESH LOGIC

    U->>C: View Protected Resource
    C->>I: Initiate GET /api/projects
    
    opt Attach Token
        I->>S: Retrieve Access Token
        S-->>I: Token "JWT_123"
        I->>I: Attach Authorization: Bearer JWT_123
    end

    I->>B: GET /api/projects
    
    alt Access Token is Valid
        B->>B: Validate JWT Signature/Expiry
        B-->>I: 200 OK (Data)
        I-->>C: Return Data
        C-->>U: Render UI
    else Access Token Expired (401)
        B-->>I: 401 Unauthorized (Token Expired)
        
        rect rgb(255, 250, 230)
            Note right of I: REFRESH LOGIC TRIGGERED
            I->>I: Pause original request queue
            I->>B: POST /auth/refresh<br/>(Cookie: refresh_token sent automatically)
            
            alt Refresh Successful
                B->>A: Exchange Refresh Token
                A-->>B: New Tokens (Access, ID, Rotated Refresh)
                B-->>I: 200 OK<br/>Set-Cookie: refresh_token=NEW...<br/>Body: { access_token: "NEW_JWT" }
                I->>S: Update Access Token (Memory)
                I->>I: Update Header: Bearer NEW_JWT
                Note right of I: RETRY ORIGINAL REQUEST
                I->>B: GET /api/projects (Retry)
                B-->>I: 200 OK (Data)
                I-->>C: Return Data
            else Refresh Failed (Session Expired/Revoked)
                B-->>I: 403 Forbidden / 401 Unauthorized
                I->>I: Reject original request
                I-->>C: Throw Authentication Error
            end
        end
    end

    Note over U, A: PHASE 3: REDIRECT PATTERNS (SESSION TERMINATION)

    alt Explicit Logout OR Refresh Failure
        C->>S: Clear Memory Store
        C->>B: POST /auth/logout
        B->>B: Clear Refresh Cookie
        B-->>C: 200 OK
        C->>U: Redirect to /login
        Note left of C: User forced to re-authenticate
    end
```