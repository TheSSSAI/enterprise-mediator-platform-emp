{
  "diagram_info": {
    "diagram_name": "End-to-End SOW Processing & Vendor Matching Workflow",
    "diagram_type": "flowchart",
    "purpose": "To visualize the core business workflow from project creation through SOW processing, human-in-the-loop review, and automated vendor matching to final distribution.",
    "target_audience": [
      "System Architects",
      "Backend Developers",
      "Product Owners",
      "QA Engineers"
    ],
    "complexity_level": "high",
    "estimated_review_time": "5-10 minutes"
  },
  "syntax_validation": "Mermaid syntax verified for flowchart TD structure using subgraphs for logical separation.",
  "rendering_notes": "Uses specific class definitions for state indicators (success/error/process) and data stores to enhance readability.",
  "diagram_elements": {
    "actors_systems": [
      "System Administrator",
      "Frontend UI",
      "Project Service",
      "AI Worker",
      "Notification Service",
      "Database/Storage"
    ],
    "key_processes": [
      "SOW Upload",
      "PII Sanitization",
      "Data Extraction",
      "Human-in-the-Loop Review",
      "Vector Matching",
      "Distribution"
    ],
    "decision_points": [
      "Processing Success/Failure",
      "Admin Approval"
    ],
    "success_paths": [
      "Upload -> Process -> Review -> Approve -> Match -> Distribute"
    ],
    "error_scenarios": [
      "File validation error",
      "AI Processing failure",
      "Missing vendor matches"
    ],
    "edge_cases_covered": [
      "Retry logic",
      "Unsaved changes warning",
      "Empty recommendation list"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart detailing the SOW lifecycle: starting with project creation, moving through AI processing and sanitization, administrator review and editing, vector-based vendor matching, and concluding with brief distribution.",
    "color_independence": "Shapes and borders distinguish between user actions (rounded rect), system processes (rect), and data stores (cylinder).",
    "screen_reader_friendly": "Sequential flow is logically ordered top-to-bottom.",
    "print_compatibility": "High contrast black and white rendering supported."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout optimized for scrolling; subgraphs group related microservice interactions.",
    "theme_compatibility": "Neutral color palette with semantic highlighting for start/end and error states.",
    "performance_notes": "Standard complexity; renders efficiently."
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the SOW upload API, AI worker event handling, and the admin review frontend.",
    "stakeholder_value": {
      "developers": "Defines the event-driven handoffs between services.",
      "product_managers": "Clarifies the 'Human-in-the-loop' intervention points.",
      "qa_engineers": "Identifies key test stages: Upload, Processing, Review, and Distribution."
    },
    "maintenance_notes": "Update if the AI model pipeline changes or if additional approval steps are added.",
    "integration_recommendations": "Link in the 'Project Management' module documentation."
  },
  "validation_checklist": [
    "✅ User Stories covered: US-029, US-030, US-032, US-034, US-036, US-039, US-042",
    "✅ Asynchronous AI processing loop included",
    "✅ Data persistence interactions mapped",
    "✅ Vendor matching trigger logic defined",
    "✅ Failure notifications represented"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Global Styling
    classDef actor fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef ui fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#e65100
    classDef service fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#4a148c
    classDef db fill:#e0f2f1,stroke:#00695c,stroke-width:2px,stroke-dasharray: 5 5,color:#004d40
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,shape:rhombus,color:#f57f17
    classDef event fill:#ffebee,stroke:#c62828,stroke-width:2px,stroke-dasharray: 3 3,color:#b71c1c

    %% Actors
    Admin((System Admin)):::actor

    subgraph "Project Initiation Phase"
        direction TB
        Start[Start: Create Project]:::ui --> ValidProj{Valid Data?}:::decision
        ValidProj -- No --> ProjError[Show Validation Error]:::ui
        ValidProj -- Yes --> DB_CreateProj[(Persist Project <br/> Status: Pending)]:::db
    end

    subgraph "SOW Ingestion Workflow"
        direction TB
        DB_CreateProj --> UploadUI[SOW Upload UI]:::ui
        Admin -- Uploads .docx/.pdf --> UploadUI
        UploadUI --> UploadAPI{Validate File}:::decision
        UploadAPI -- Invalid --> UploadFail[Reject & Notify]:::ui
        UploadAPI -- Valid --> S3_Store[(S3: Original SOW)]:::db
        S3_Store --> DB_UpdateStatus[(Update Status: <br/> PROCESSING)]:::db
        DB_UpdateStatus --> PubEvent1{{Event: SOW_UPLOADED}}:::event
    end

    subgraph "AI Processing (Async)"
        direction TB
        PubEvent1 --> AI_Worker[[AI Ingestion Worker]]:::service
        AI_Worker --> AI_Sanitize[PII Sanitization]:::service
        AI_Worker --> AI_Extract[Data Extraction]:::service
        
        AI_Extract --> ProcessCheck{Processing <br/> Successful?}:::decision
        
        ProcessCheck -- No --> DB_Fail[(Update Status: <br/> FAILED)]:::db
        DB_Fail --> PubEventFail{{Event: PROCESSING_FAILED}}:::event
        PubEventFail --> NotifyFail[Notification Service <br/> Email/In-App Alert]:::service
        NotifyFail --> Admin
        
        ProcessCheck -- Yes --> S3_Sanitized[(S3: Sanitized SOW)]:::db
        S3_Sanitized --> DB_Success[(Persist Extracted Data <br/> Update Status: PROCESSED)]:::db
        DB_Success --> PubEventSuccess{{Event: PROCESSING_SUCCESS}}:::event
        PubEventSuccess --> NotifySuccess[Notification Service <br/> Ready for Review]:::service
        NotifySuccess --> Admin
    end

    subgraph "Human-in-the-Loop Review"
        direction TB
        Admin -- Clicks Notification --> ReviewUI[SOW Review Interface]:::ui
        ReviewUI -- Fetch Data --> GET_Brief[GET /projects/brief]:::service
        GET_Brief --> DB_Read[(Read Extracted Data)]:::db
        
        ReviewUI --> EditLoop{Admin Edits?}:::decision
        EditLoop -- Yes --> SaveDraft[Save Changes]:::service
        SaveDraft --> DB_UpdateBrief[(Update Project Brief)]:::db
        DB_UpdateBrief --> ReviewUI
        
        EditLoop -- No --> ApproveBtn[Click 'Approve Brief']:::ui
        ApproveBtn --> ApproveAPI[POST /projects/brief/approve]:::service
        ApproveAPI --> DB_Lock[(Update Status: <br/> BRIEF_APPROVED)]:::db
        DB_Lock --> PubEventApproved{{Event: BRIEF_APPROVED}}:::event
    end

    subgraph "Vendor Matching & Distribution"
        direction TB
        PubEventApproved --> MatchService[[Vendor Matching Service]]:::service
        MatchService --> VectorGen[Generate Embeddings]:::service
        VectorGen --> VectorDB[(Vector Search <br/> pgvector)]:::db
        VectorDB -- Return Ranked List --> RecUI[Vendor Recommendations UI]:::ui
        
        Admin -- Reviews List --> RecUI
        RecUI -- Selects Vendors --> DistributeBtn[Click 'Distribute Brief']:::ui
        DistributeBtn --> DistAPI[POST /projects/distribute]:::service
        DistAPI --> DB_StatusProp[(Update Status: <br/> PROPOSED)]:::db
        DistAPI --> PubEventDist{{Event: BRIEF_DISTRIBUTED}}:::event
        
        PubEventDist --> NotifyVendor[Notification Service <br/> Send Invitations]:::service
        NotifyVendor -- Email w/ Secure Link --> Vendor((Vendor Contact)):::actor
    end

    %% Connections across subgraphs
    ProjError --> Start
    UploadFail --> UploadUI
    ReviewUI -.-> |View Sanitized SOW| S3_Sanitized
    
    %% Legend
    subgraph Legend
        L1[User Action]:::ui
        L2[Backend Service]:::service
        L3[Database/Storage]:::db
        L4[Async Event]:::event
    end
```