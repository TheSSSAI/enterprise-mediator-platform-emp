# Enterprise Mediator Platform (EMP) - Enterprise Architecture Documentation

## Executive Summary

This document outlines the comprehensive enterprise architecture for the Enterprise Mediator Platform (EMP), a cloud-native, multi-tenant SaaS application designed to automate and intelligently manage the core workflows of a consulting intermediary business. The system replaces manual, error-prone processes with a streamlined, event-driven, and data-centric platform, acting as the central nervous system for the business.

The architecture is founded on a **Polyrepo, Microservices** approach, decomposed from an initial monolithic structure. This strategic decision was driven by the need to support parallel development by domain-aligned teams, enable independent scaling of components, and create strong security boundaries around sensitive data and complex logic, particularly for financial and AI processing domains. The chosen technology stack, centered around a full-stack TypeScript ecosystem (NestJS, React) and PostgreSQL, provides a cohesive, high-performance, and maintainable foundation. The adoption of this distributed architecture directly addresses the platform's core requirements for scalability, reliability, and security, delivering business value by accelerating feature delivery, improving system resilience, and simplifying compliance efforts.

## Solution Architecture Overview

EMP is designed as a distributed system of collaborating services deployed on AWS, adhering to modern cloud-native principles.

*   **Architectural Style**: A **Microservices Architecture** is employed, where the system is decomposed into a set of independent, domain-oriented services. This is complemented by an **Event-Driven Architecture** for handling asynchronous, long-running processes like SOW ingestion, which decouples components and enhances system resilience.

*   **Technology Stack**:
    *   **Frontend**: A responsive Single Page Application (SPA) built with **Next.js 14 (React 18)** and **TypeScript**, styled with **Tailwind CSS** and using **Radix UI** for accessible, headless components.
    *   **Backend**: Backend services are built with **Node.js (NestJS)** and **C# (.NET 8)**, providing a robust, type-safe, and high-performance environment for APIs and background workers.
    *   **Database**: **PostgreSQL 16** (via AWS RDS) serves as the primary relational database. The **`pgvector`** extension is a key component, enabling efficient and integrated semantic search capabilities without the need for a separate vector database.
    *   **Messaging & Caching**: **RabbitMQ** provides the asynchronous message bus for inter-service communication. **Redis** (via AWS ElastiCache) is used for distributed caching to meet low-latency performance targets.
    *   **Infrastructure**: The entire platform is deployed on **AWS** using **Docker** for containerization and **AWS EKS** for orchestration. Infrastructure is managed declaratively via **Terraform**.

*   **Integration Approach**: Services communicate via a combination of synchronous and asynchronous patterns. The frontend interacts exclusively with a secure **API Gateway**, which acts as a facade for the internal backend. Internally, services communicate via direct API calls for queries and an asynchronous message bus for commands and events, enabling loose coupling and resilience.

## Repository Architecture Strategy

The project has evolved from a single monorepo to a structured **Polyrepo** model to support the microservices architecture and foster team autonomy. The decomposition strategy was hybrid, based on both architectural layers and business domains.

*   **Architectural Layering**: Clear separation is maintained between the **Presentation Layer** (Frontend App, UI Library), **Application Layer** (API Gateway, Backend Services), **Domain & Integration Layer** (Shared Models, Contracts), and **Infrastructure Layer** (Terraform).
*   **Domain Partitioning**: Backend services are partitioned by business capability (e.g., `emp-project-management-service`, `emp-financial-service`), allowing domain-focused teams to own their codebase, data, and release cycles.
*   **Shared Dependencies**: Cross-cutting concerns are managed through versioned, shared libraries (`emp-domain-models`, `emp-shared-contracts`, `emp-core-shared-kernel`). This promotes code reuse while preventing the tight coupling characteristic of a monorepo.

This optimized structure significantly improves the development workflow by enabling independent development, testing, and deployment of components. It reduces cognitive load on developers, clarifies ownership, and enhances overall system maintainability and scalability.

## System Architecture Diagrams

### Repository Dependency Architecture

The following diagram illustrates the polyrepo structure, the dependencies between repositories, and their organization into architectural layers.



### Component Integration Patterns

This sequence diagram details the `Request-Reply`, `File Transfer`, and `Publish-Subscribe` patterns during a critical SOW Upload workflow, showing the interaction between the frontend, API gateway, message bus, and the asynchronous AI worker.



## Repository Catalog

**Presentation Layer**
*   **`emp-frontend-webapp` (REPO-FE-WEBAPP)**: The primary client-facing Next.js SPA. Responsible for all UI rendering, client-side state, and interaction with the API Gateway.
*   **`emp-ui-component-library` (REPO-LIB-UICOMP)**: A reusable React component library based on Radix UI and Tailwind CSS. Published as an NPM package to ensure UI consistency and accessibility.

**Application Gateway & Services**
*   **`emp-api-gateway` (REPO-GW-API)**: The main backend entry point (BFF). Handles authentication, authorization, rate limiting, and routing to internal microservices.
*   **`emp-project-management-service` (REPO-SVC-PROJECT)**: Core microservice managing the Project, SOW, and Proposal lifecycles, including the vendor matching logic.
*   **`emp-financial-service` (REPO-SVC-FINANCIAL)**: Secure microservice for all financial operations: invoicing, payments (Stripe), payouts (Wise), and ledgers.
*   **`emp-user-management-service` (REPO-SVC-USER)**: Microservice acting as the system of record for Client, Vendor, and Internal User profiles and RBAC data.
*   **`emp-ai-processing-worker` (REPO-SVC-AIWORKER)**: Asynchronous background worker service that consumes events to perform the SOW processing pipeline (PII sanitization, data extraction, vector embedding).

**Shared Libraries & Infrastructure**
*   **`emp-domain-models` (REPO-LIB-DOMAIN)**: A dependency-free library containing the core business entities and domain logic (DDD Aggregates). The single source of truth for the business model.
*   **`emp-shared-contracts` (REPO-LIB-CONTRACTS)**: Lightweight library defining DTOs and event schemas for type-safe communication between all services and the frontend.
*   **`emp-core-shared-kernel` (REPO-LIB-SHARED KERNEL)**: A shared library for backend services, providing common utilities like logging configuration, resiliency policies (Polly), and base repository patterns.
*   **`emp-platform-infrastructure` (REPO-IAC-INFRA)**: Contains all Infrastructure as Code (Terraform) for provisioning the AWS environment (EKS, RDS, SQS, etc.).

## Integration Architecture

System components interact through well-defined contracts and patterns to ensure loose coupling and scalability.

*   **Interface Contracts**: Communication is formalized by the `emp-shared-contracts` repository. This package provides C# records for backend DTOs and event schemas, which are transpiled to TypeScript types for type-safe consumption by the frontend.
*   **Synchronous Communication**: The `emp-frontend-webapp` makes **Request-Reply** RESTful API calls over HTTPS to the `emp-api-gateway`. The gateway, in turn, uses internal HTTP/gRPC clients with resiliency policies (Retry, Circuit Breaker) to communicate with backend microservices.
*   **Asynchronous Communication**: The **Publish-Subscribe** pattern is used for decoupling long-running or non-critical tasks. The `emp-api-gateway` publishes an `SowUploadedEvent` to **RabbitMQ**. The `emp-ai-processing-worker` subscribes to these events to begin its workflow. Similarly, the `emp-project-management-service` publishes events like `ProjectAwardedEvent` which are consumed by the `emp-financial-service`.
*   **File Transfer**: SOW documents and CSV imports are handled via `multipart/form-data` HTTP uploads to a dedicated endpoint on the `emp-api-gateway`, which then streams the file to AWS S3 before publishing an event.

## Technology Implementation Framework

*   **Backend (.NET)**: Services are built following **Clean Architecture** principles, separating domain logic from application and infrastructure concerns. **Domain-Driven Design (DDD)** is applied, with core logic encapsulated in the `emp-domain-models` library. The **CQRS pattern** (using MediatR) is used within services to separate command (write) and query (read) pathways.
*   **Frontend (React)**: A **component-based architecture** is used, with a clear separation between presentational components (from the UI library) and container components that manage state and data fetching. **Zustand** is used for simple, effective global state management.
*   **Database (PostgreSQL)**: Data access is managed via the **Repository Pattern**, abstracted using Entity Framework Core. The `pgvector` extension is used for **semantic search**, where vector embeddings of SOW requirements are compared against vendor skill embeddings using cosine similarity.

## Performance & Scalability Architecture

The architecture is designed to meet stringent performance and scalability requirements.

*   **Performance**: Low-latency API response times (p95 < 250ms) are achieved through efficient database queries, the use of a **Redis** distributed cache for frequently accessed data, and a lightweight API Gateway. Frontend LCP targets (< 2.5s) are met by leveraging Next.js server components and image optimization.
*   **Scalability**: The system is designed to scale horizontally. The Kubernetes-based deployment on **AWS EKS** uses the **Horizontal Pod Autoscaler (HPA)** to automatically scale the number of service pods based on CPU and memory load, as specified in REQ-NFR-005. The `emp-ai-processing-worker` is an independent deployment that can be scaled separately based on message queue depth to handle high volumes of SOW uploads.
*   **Reliability**: High availability is achieved by deploying services across multiple AWS Availability Zones. The event-driven nature of the SOW processing pipeline ensures that failures in the AI services do not impact the core user-facing application.

## Development & Deployment Strategy

The polyrepo structure directly informs the development and deployment strategy.

*   **Team Organization**: Teams are aligned with business domains (e.g., Project Workflow Team, Financials Team, Platform/SRE Team), with each team owning a set of microservice and library repositories.
*   **Development Workflow**: Each repository has its own CI pipeline configured in **GitHub Actions**. On a pull request, the pipeline runs static analysis, unit tests, and builds the deployable artifact (e.g., Docker image). Shared libraries are versioned using SemVer and published to private NuGet/NPM registries.
*   **Deployment Architecture**: A **GitOps** approach is favored. Merging to the main branch triggers a CD pipeline that publishes the new Docker image and updates the Kubernetes deployment manifests in a separate configuration repository. This triggers an automated, rolling deployment to the EKS cluster, ensuring zero downtime.

## Architecture Decision Records

*   **ADR-001: Adopt a Microservices and Polyrepo Architecture**
    *   **Decision**: Decompose the initial monorepo into a polyrepo structure with independent, domain-oriented microservices.
    *   **Rationale**: To enable parallel development, independent deployment, and specialized scaling. This improves team velocity, clarifies ownership, and establishes strong security boundaries, which is critical for financial and AI components.
*   **ADR-002: Use PostgreSQL with `pgvector` for Semantic Search**
    *   **Decision**: Utilize the `pgvector` extension within the primary PostgreSQL database instead of introducing a separate, dedicated vector database (e.g., Pinecone, Weaviate).
    *   **Rationale**: This approach significantly reduces operational complexity and cost by leveraging the existing, managed data store. It simplifies data synchronization and is sufficient for the scale and performance requirements of the vendor matching feature (REQ-FUN-002).
*   **ADR-003: Asynchronous SOW Processing via an Event-Driven Worker**
    *   **Decision**: The AI-powered SOW ingestion and analysis process will be fully asynchronous, triggered by an event and executed by a dedicated background worker service.
    *   **Rationale**: SOW processing is a long-running, resource-intensive task. Decoupling it from the synchronous API request ensures the user-facing application remains responsive and reliable. This pattern also improves resilience, as failed processing jobs can be retried or dead-lettered without affecting the user.