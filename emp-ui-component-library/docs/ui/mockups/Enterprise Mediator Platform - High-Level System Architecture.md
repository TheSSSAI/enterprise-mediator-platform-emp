{
  "diagram_info": {
    "diagram_name": "Enterprise Mediator Platform - High-Level System Architecture",
    "diagram_type": "flowchart",
    "purpose": "To provide a comprehensive architectural overview of the Enterprise Mediator Platform, illustrating the interaction between user personas, the frontend application, backend microservices, data storage, and external integrations.",
    "target_audience": [
      "System Architects",
      "Lead Developers",
      "Stakeholders",
      "DevOps Engineers"
    ],
    "complexity_level": "high",
    "estimated_review_time": "10-15 minutes"
  },
  "diagram_elements": {
    "actors_systems": [
      "System Administrator",
      "Finance Manager",
      "Client Contact",
      "Vendor Contact",
      "Next.js Frontend",
      "API Gateway",
      "Core Services (Project, Vendor, Finance, User)",
      "Specialized Services (AI Ingestion, Notification)",
      "Data Stores (PostgreSQL, Redis, S3)",
      "External Integrations (Stripe, OpenAI, AWS SES, Cognito)"
    ],
    "key_processes": [
      "User Authentication & RBAC",
      "SOW Upload & AI Processing",
      "Vendor Matching via Vector Search",
      "Proposal Submission & Management",
      "Financial Transactions (Invoicing/Payouts)",
      "Audit Logging"
    ],
    "decision_points": [
      "Auth Validation",
      "Role-Based Access Control",
      "SOW Processing Success/Failure",
      "Payment Gateway Response"
    ],
    "success_paths": [
      "End-to-end Project Lifecycle",
      "Secure Payment Processing",
      "Automated Vendor Matching"
    ],
    "error_scenarios": [
      "External API Failures",
      "AI Processing Errors",
      "Payment Declines"
    ],
    "edge_cases_covered": [
      "Asynchronous processing delays",
      "Service isolation"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "High-level architecture diagram showing users connecting to a React frontend, which communicates via an API Gateway to backend microservices. Key services include Project, Vendor, Finance, and AI Ingestion. Data is stored in PostgreSQL and S3. External integrations include Stripe, OpenAI, and AWS SES.",
    "color_independence": "Nodes are distinguished by shape and grouping (subgraphs) in addition to color styling.",
    "screen_reader_friendly": "Flow hierarchy is logical: Users -> Frontend -> Gateway -> Services -> Data/External.",
    "print_compatibility": "High contrast lines and text ensure readability in grayscale."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout (TB) optimized for scrolling on standard screens.",
    "theme_compatibility": "Neutral styling compatible with light and dark modes.",
    "performance_notes": "Uses subgraphs to cluster related components for faster cognitive processing."
  },
  "usage_guidelines": {
    "when_to_reference": "During onboarding, architectural reviews, and when planning cross-service features.",
    "stakeholder_value": {
      "developers": "Understanding service boundaries and data flow.",
      "designers": "Contextualizing where UI interactions trigger backend processes.",
      "product_managers": "Visualizing external dependencies and system capabilities.",
      "qa_engineers": "Identifying integration testing points."
    },
    "maintenance_notes": "Update when new microservices are added or external integrations change.",
    "integration_recommendations": "Include in the system README and architectural design document (ADD)."
  },
  "validation_checklist": [
    "✅ All user personas included",
    "✅ Frontend and Backend separation clear",
    "✅ Critical external integrations (Stripe, OpenAI) mapped",
    "✅ Data storage layers defined",
    "✅ Mermaid syntax validated",
    "✅ Logical flow from User to Data"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TB
    %% Definitions and Styling
    classDef actor fill:#e1f5fe,stroke:#01579b,stroke-width:2px,color:#000
    classDef frontend fill:#fff3e0,stroke:#e65100,stroke-width:2px,color:#000
    classDef gateway fill:#f3e5f5,stroke:#4a148c,stroke-width:2px,color:#000
    classDef service fill:#e8f5e9,stroke:#1b5e20,stroke-width:2px,color:#000
    classDef db fill:#e0e0e0,stroke:#424242,stroke-width:2px,stroke-dasharray: 5 5,color:#000
    classDef external fill:#ffebee,stroke:#b71c1c,stroke-width:2px,color:#000

    %% User Layer
    subgraph Users ["User Layer"]
        direction LR
        Admin([System Admin]):::actor
        Finance([Finance Manager]):::actor
        Client([Client Contact]):::actor
        Vendor([Vendor Contact]):::actor
    end

    %% Frontend Layer
    subgraph Presentation ["Presentation Layer"]
        WebApp["Next.js SPA\n(Radix UI + Tailwind)"]:::frontend
        CDN["CDN / Edge"]:::frontend
    end

    %% API Gateway Layer
    subgraph EntryPoint ["Entry Point"]
        APIGW["API Gateway / Load Balancer\n(Auth Middleware & Rate Limiting)"]:::gateway
    end

    %% Backend Services Layer
    subgraph Backend ["Application Services Layer (EKS/Containerized)"]
        direction TB
        
        subgraph Core_Services ["Core Domain Services"]
            SvcUser["User Service\n(Profiles, Org Mgmt)"]:::service
            SvcProject["Project Service\n(Lifecycle, Proposals)"]:::service
            SvcVendor["Vendor Service\n(Skills, Vetting)"]:::service
            SvcFinance["Payment Service\n(Invoices, Payouts, Ledger)"]:::service
        end

        subgraph Async_Services ["Async & Utility Services"]
            SvcAI["AI Ingestion Service\n(SOW Parsing, Embeddings)"]:::service
            SvcNotify["Notification Service\n(Email, In-App, Webhook)"]:::service
            SvcAudit["Audit Log Service\n(Compliance, Security)"]:::service
        end
        
        MsgQueue["Message Bus\n(RabbitMQ / AWS SNS+SQS)"]:::service
    end

    %% Data Layer
    subgraph Data ["Data Persistence Layer"]
        DB_Main[("PostgreSQL\n(Transactional Data)")]:::db
        DB_Vector[("PostgreSQL + pgvector\n(Vector Embeddings)")]:::db
        DB_Cache[("Redis\n(Caching & Sessions)")]:::db
        S3_Bucket[("AWS S3\n(Documents & SOWs)")]:::db
        DB_Audit[("OpenSearch / Elastic\n(Audit Logs)")]:::db
    end

    %% External Systems
    subgraph External ["External Integrations"]
        Ext_Auth["AWS Cognito\n(Identity Provider)"]:::external
        Ext_AI["OpenAI API\n(LLM & Embeddings)"]:::external
        Ext_Pay["Stripe Connect / Wise\n(Payment Gateway)"]:::external
        Ext_Email["AWS SES\n(Email Delivery)"]:::external
    end

    %% Relationships - Users to Frontend
    Users --> CDN
    CDN --> WebApp

    %% Relationships - Frontend to Gateway
    WebApp -- "HTTPS/REST/GraphQL" --> APIGW

    %% Relationships - Gateway to Services
    APIGW --> SvcUser
    APIGW --> SvcProject
    APIGW --> SvcVendor
    APIGW --> SvcFinance
    APIGW -- "Async Trigger" --> MsgQueue

    %% Service Inter-communication
    Core_Services <--> MsgQueue
    MsgQueue --> SvcAI
    MsgQueue --> SvcNotify
    MsgQueue --> SvcAudit

    %% Service to Data
    SvcUser & SvcProject & SvcVendor & SvcFinance --> DB_Main
    SvcAI --> DB_Vector
    SvcAI --> S3_Bucket
    SvcAudit --> DB_Audit
    Core_Services -.-> DB_Cache

    %% Service to External
    SvcUser --> Ext_Auth
    SvcAI --> Ext_AI
    SvcFinance --> Ext_Pay
    SvcNotify --> Ext_Email

    %% Specific High-Value Flows
    note_sow["SOW Processing Flow"] -.-> SvcProject
    SvcProject -- "Upload Event" --> MsgQueue
    MsgQueue -- "Consume Event" --> SvcAI
    SvcAI -- "Extract & Sanitize" --> Ext_AI
    SvcAI -- "Store Result" --> DB_Vector

    note_pay["Financial Flow"] -.-> SvcFinance
    SvcFinance -- "Charge/Payout" --> Ext_Pay
    Ext_Pay -- "Webhook" --> APIGW
```