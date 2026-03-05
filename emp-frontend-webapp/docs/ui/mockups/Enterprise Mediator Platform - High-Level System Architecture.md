{
  "diagram_info": {
    "diagram_name": "Enterprise Mediator Platform - High-Level System Architecture",
    "diagram_type": "flowchart",
    "purpose": "To visualize the high-level structural architecture of the Enterprise Mediator Platform, illustrating the interactions between user interfaces, backend microservices, data persistence layers, asynchronous processing queues, and external third-party integrations.",
    "target_audience": [
      "System Architects",
      "DevOps Engineers",
      "Backend Developers",
      "Stakeholders"
    ],
    "complexity_level": "high",
    "estimated_review_time": "10-15 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for TB (Top-Bottom) layout. Uses subgraphs to delineate trust boundaries (Client vs. Cloud Infrastructure vs. External Services).",
  "diagram_elements": {
    "actors_systems": [
      "Users (Admin, Client, Vendor)",
      "Frontend SPA (Next.js/Radix UI)",
      "API Gateway / Load Balancer",
      "EMP Backend API Services",
      "AI Processing Worker",
      "Notification Service",
      "PostgreSQL (Transactional DB)",
      "OpenSearch/pgvector (Search/Audit DB)",
      "RabbitMQ/SQS (Message Broker)",
      "AWS S3 (Object Storage)"
    ],
    "external_integrations": [
      "AWS Cognito (Auth)",
      "AWS SES (Email)",
      "Stripe Connect / Wise (Payments)",
      "OpenAI API (LLM/Embeddings)",
      "AWS Comprehend (PII Redaction)"
    ],
    "key_flows": [
      "User Authentication",
      "SOW Upload & Async Processing",
      "Vendor Semantic Search",
      "Financial Transaction Processing",
      "Notification Dispatch"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "High-level architecture diagram showing users connecting to the frontend, which communicates via API Gateway to backend services. Backend services connect to databases, message queues, and external APIs like Stripe and OpenAI.",
    "color_independence": "Nodes are shaped and grouped logically; colors are used to distinguish internal vs external systems but structure conveys the primary meaning.",
    "screen_reader_friendly": "Flow direction and subgraph grouping provide logical traversal order.",
    "print_compatibility": "High contrast borders and text ensure readability in grayscale."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout optimized for scrolling; subgraphs group related components to prevent visual clutter.",
    "theme_compatibility": "Neutral styling compatible with standard light/dark modes.",
    "performance_notes": "Standard flowchart rendering."
  },
  "usage_guidelines": {
    "when_to_reference": "During system onboarding, architectural reviews, and infrastructure planning.",
    "stakeholder_value": {
      "developers": "Understanding service boundaries and inter-service communication.",
      "ops": "Identifying infrastructure components and deployment targets.",
      "product_managers": "Visualizing dependencies on external services (AI, Payments)."
    },
    "maintenance_notes": "Update when new microservices are added or external integrations change.",
    "integration_recommendations": "Include in the 'System Architecture' section of the technical design document."
  },
  "validation_checklist": [
    "✅ All external integrations (Stripe, OpenAI, AWS) represented",
    "✅ Asynchronous processing path (Queue -> Worker) included",
    "✅ Data persistence layers (SQL, NoSQL/Vector, Object Store) distinct",
    "✅ User personas correctly mapped to entry points",
    "✅ Security/Auth layer (Cognito) included",
    "✅ Syntax validated"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TB
    %% --- User Layer ---
    subgraph Users ["User Layer"]
        Admin[System Administrator]
        FinMgr[Finance Manager]
        Client[Client Contact]
        Vendor[Vendor Contact]
    end

    %% --- Frontend Layer ---
    subgraph Frontend ["Presentation Layer"]
        SPA["Frontend SPA<br/>(Next.js + Radix UI)"]
    end

    %% --- Cloud Infrastructure ---
    subgraph CloudInfra ["AWS Cloud Infrastructure (EKS Cluster)"]
        
        %% Ingress
        Gateway["API Gateway / Load Balancer<br/>(TLS Termination)"]

        %% Services
        subgraph Services ["Application Services"]
            CoreAPI["EMP Backend API<br/>(Core Business Logic)"]
            AuthSvc["Auth Middleware<br/>(RBAC Enforcement)"]
            NotifySvc["Notification Service"]
            PaymentSvc["Payment Service"]
        end

        %% Async Processing
        subgraph Async ["Asynchronous Processing"]
            MsgBroker["Message Broker<br/>(RabbitMQ / AWS SQS)"]
            AIWorker["AI Processing Worker<br/>(SOW Sanitization & Extraction)"]
        end

        %% Data Layer
        subgraph Data ["Data Persistence Layer"]
            PrimaryDB[("PostgreSQL<br/>(Transactional Data)")]
            VectorDB[("OpenSearch / pgvector<br/>(Search & Audit Logs)")]
            ObjStore[("AWS S3<br/>(Documents/SOWs)")]
            Cache[("Redis<br/>(Session/Data Cache)")]
        end
    end

    %% --- External Services ---
    subgraph External ["External Integrations"]
        Cognito["AWS Cognito<br/>(Identity Provider)"]
        Stripe["Stripe Connect / Wise<br/>(Payment Gateway)"]
        SES["AWS SES<br/>(Email Delivery)"]
        OpenAI["OpenAI API<br/>(LLM & Embeddings)"]
        Comprehend["AWS Comprehend<br/>(PII Detection)"]
    end

    %% --- Relationships ---

    %% User to Frontend
    Admin & FinMgr & Client & Vendor -->|HTTPS| SPA

    %% Frontend to Gateway
    SPA -->|REST/GraphQL| Gateway

    %% Gateway to Services
    Gateway --> AuthSvc
    AuthSvc --> CoreAPI

    %% Service to Service / Infra
    CoreAPI -->|Read/Write| PrimaryDB
    CoreAPI -->|Read/Write| Cache
    CoreAPI -->|Search/Audit| VectorDB
    CoreAPI -->|Upload/Download| ObjStore
    CoreAPI -->|Publish Events| MsgBroker

    %% Payment Service Interactions
    CoreAPI <--> PaymentSvc
    PaymentSvc -->|API Calls| Stripe
    Stripe -.->|Webhooks| Gateway

    %% Auth Interactions
    AuthSvc <-->|Verify Tokens| Cognito

    %% Async Worker Flow
    MsgBroker -->|Consume Tasks| AIWorker
    MsgBroker -->|Consume Events| NotifySvc
    
    %% AI Worker Flow
    AIWorker -->|Fetch SOW| ObjStore
    AIWorker -->|PII Check| Comprehend
    AIWorker -->|Extraction/Embeddings| OpenAI
    AIWorker -->|Store Results| PrimaryDB
    AIWorker -->|Store Embeddings| VectorDB

    %% Notification Flow
    NotifySvc -->|Send Email| SES

    %% Styling
    classDef user fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#000
    classDef frontend fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000
    classDef service fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef data fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#000
    classDef external fill:#eeeeee,stroke:#616161,stroke-width:2px,stroke-dasharray: 5 5,color:#000
    classDef async fill:#e0f7fa,stroke:#006064,stroke-width:2px,color:#000

    class Admin,FinMgr,Client,Vendor user
    class SPA frontend
    class Gateway,CoreAPI,AuthSvc,PaymentSvc service
    class MsgBroker,AIWorker,NotifySvc async
    class PrimaryDB,VectorDB,ObjStore,Cache data
    class Cognito,Stripe,SES,OpenAI,Comprehend external

    %% Link Styling
    linkStyle default stroke:#333,stroke-width:1px
```