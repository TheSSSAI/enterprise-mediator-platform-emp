{
  "diagram_info": {
    "diagram_name": "Project Lifecycle State Machine",
    "diagram_type": "stateDiagram-v2",
    "purpose": "Documents the definitive state transitions for the core Project entity, including triggers, guard conditions, and side effects like financial events.",
    "target_audience": [
      "backend developers",
      "product managers",
      "QA engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid stateDiagram-v2 syntax verified",
  "rendering_notes": "Optimized for vertical flow with clear separation of happy path and exception states",
  "diagram_elements": {
    "actors_systems": [
      "System Admin",
      "Client",
      "Vendor",
      "Payment Gateway"
    ],
    "key_processes": [
      "SOW Processing",
      "Vendor Matching",
      "Invoicing",
      "Dispute Resolution"
    ],
    "decision_points": [
      "Brief Approval",
      "Proposal Acceptance",
      "Payment Confirmation",
      "Dispute Resolution"
    ],
    "success_paths": [
      "Pending -> Proposed -> Awarded -> Active -> Completed"
    ],
    "error_scenarios": [
      "Cancellation at various stages",
      "Disputes requiring mediation"
    ],
    "edge_cases_covered": [
      "Reverting from On Hold",
      "Resolving Disputes"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram showing the lifecycle of a project from Pending creation through Proposal and Active phases to Completion, including exception states like On Hold, Cancelled, and Disputed.",
    "color_independence": "States are differentiated by position and label, not just color",
    "screen_reader_friendly": "All transitions have clear text labels describing the trigger event",
    "print_compatibility": "High contrast lines and text suitable for black and white printing"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout fits standard documentation viewports",
    "theme_compatibility": "Neutral styling works with light/dark modes",
    "performance_notes": "Standard complexity, renders instantly"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing state transition logic in the Project Service or designing UI status indicators.",
    "stakeholder_value": {
      "developers": "Defines valid state transitions and triggers for the state machine implementation",
      "designers": "Maps necessary UI actions (buttons/modals) available in each state",
      "product_managers": "Visualizes the business process flow and constraints",
      "QA_engineers": "Provides a map for testing valid and invalid state transitions"
    },
    "maintenance_notes": "Update if new intermediate states (e.g., 'Draft') are introduced or if financial triggers change.",
    "integration_recommendations": "Include in the Project Service technical design document."
  },
  "validation_checklist": [
    "✅ Happy path (Pending to Completed) clearly defined",
    "✅ Financial triggers (Invoice Paid) included",
    "✅ Exception states (On Hold, Cancelled, Disputed) documented",
    "✅ Mermaid syntax validated",
    "✅ Correctly reflects requirements REQ-FUN-003 and US-041"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    classDef happyPath fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef exception fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#b71c1c
    classDef terminal fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20
    classDef finalCancel fill:#f5f5f5,stroke:#616161,stroke-width:2px,color:#212121

    [*] --> Pending: Project Created
    
    state Pending ::: happyPath {
        [*] --> SOW_Upload
        SOW_Upload --> AI_Processing
        AI_Processing --> SOW_Review: Processing Complete
        SOW_Review --> Brief_Approval: Admin Edits
        Brief_Approval --> [*]: Brief Approved
        
        note right of AI_Processing
            Async Job
            (REQ-FUN-002)
        end note
    }

    Pending --> Proposed: Brief Distributed to Vendors\n(US-042)
    
    state Proposed ::: happyPath {
        [*] --> Bidding_Open
        Bidding_Open --> Proposal_Review: Proposals Received
        Proposal_Review --> [*]: Proposal Accepted
    }

    Proposed --> Awarded: Vendor Selected\n(US-054)
    
    state Awarded ::: happyPath {
        [*] --> Invoice_Generation
        Invoice_Generation --> Awaiting_Payment: Invoice Sent
        Awaiting_Payment --> [*]: Payment Confirmed
    }

    Awarded --> Active: Client Pays Invoice\n(Funds in Escrow)\n(US-059)
    
    state Active ::: happyPath {
        [*] --> In_Progress
        In_Progress --> Milestone_Review: Milestone Submitted
        Milestone_Review --> Payout_Processing: Client Approves Milestone
        Payout_Processing --> In_Progress: Vendor Paid
        Payout_Processing --> [*]: Final Deliverable Paid
    }

    Active --> Completed: All Work Finished & Paid\n(US-060)
    
    %% Exception Flows
    Active --> Disputed: Issue Raised\n(US-065)
    Disputed ::: exception --> Active: Dispute Resolved\n(Funds Released)
    Disputed --> Cancelled: Dispute Unresolved\n(Funds Returned)
    
    Active --> OnHold: Admin Pauses Project\n(US-041)
    OnHold ::: exception --> Active: Admin Resumes Project
    
    %% Cancellation Paths
    Pending --> Cancelled: Admin Cancels
    Proposed --> Cancelled: Admin Cancels
    Awarded --> Cancelled: Admin Cancels
    OnHold --> Cancelled: Admin Cancels
    
    state Completed ::: terminal
    state Cancelled ::: finalCancel

    note right of Active
        Primary Operational State
        Recurring Payouts
    end note

    note left of Disputed
        Manual Intervention Required
        Financial Hold
    end note
```