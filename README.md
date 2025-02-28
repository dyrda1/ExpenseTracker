# Expense Tracker - Deployment Guide

## 📌 Overview
This project is an **ASP.NET Web Application** with a **PostgreSQL** database deployed on a **VPS** using **Docker Swarm**. It is fully automated with **CI/CD**, secure with **SSL and firewall rules**, and monitored using **Prometheus, Loki, and Grafana**.

---

## 🚀 Getting Started

### **1️⃣ Prerequisites**
Ensure you have the following installed on your local machine:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Git](https://git-scm.com/downloads)
- [k6](https://k6.io/docs/getting-started/installation/)

### **2️⃣ Cloning the Repository**
```bash
git clone https://github.com/dyrda1/expense-tracker.git
cd expense-tracker
```

### **3️⃣ Environment Variables & Secrets**
The project uses Docker Secrets to store sensitive data. Ensure the following secrets are set on your VPS:

```bash
docker secret create db_password -
docker secret create db_conn -
docker secret create nginx_htpasswd -
```

---

## 🏗 Deployment

### **1️⃣ Setting Up the VPS**
1. Create a non-root user.
2. Enable `ufw` and allow ports `22, 80, 443`.
3. Set up **SSH hardening** (disable root login, password login, and PAM auth).
4. Install **fail2ban** for security.

### **2️⃣ Deploying the Application**
Run the following command on your VPS:
```bash
docker stack deploy -c docker-compose.yml expense_tracker
```

### **3️⃣ Checking Deployment Status**
To verify that all services are running:
```bash
docker service ls
docker ps
```

To inspect logs:
```bash
docker logs <container_id>
```

---

## 🔍 Testing

### **1️⃣ Load Testing with k6**
To test application performance, run:
```bash
cd k6
k6 run load-test.js
```

### **2️⃣ Checking Logs & Metrics**
- **Application Logs**: `docker logs app`
- **Database Logs**: `docker logs db`
- **Prometheus Metrics**: Available at `/metrics`
- **Grafana Dashboard**: `https://idyrda.site/grafana/`

---

## 🛠 Troubleshooting

**1️⃣ Restart a Service**
```bash
docker service update --force expense_tracker_app
```

**2️⃣ Scale Up/Down Services**
```bash
docker service scale expense_tracker_app=3
```

**3️⃣ Check Running Containers**
```bash
docker ps
```

**4️⃣ Remove Unused Volumes**
```bash
docker volume prune
```

---

## 📊 Monitoring & Observability

| Tool      | Purpose |
|-----------|---------|
| **Prometheus** | Metrics Collection |
| **Grafana** | Metrics Visualization |
| **Loki** | Log Aggregation |
| **Tempo** | Tracing |
| **k6** | Load Testing |

---

## 🔄 Backup Strategy
The database is automatically backed up every **24 hours** and stored in the `/backups` volume.

To restore a backup:
```bash
docker exec -i db psql -U postgres -d expense_tracker < /path/to/backup.sql
```

---

## 🔥 Future Improvements
- [ ] Automate deployment with **Ansible**.
- [ ] Add **Alerting System** for failures.
- [ ] Implement **Horizontal Scaling** with multiple VPS instances.

---

## 👨‍💻 Contributors
- [Dyrda1](https://github.com/dyrda1)

---

## 📜 License
This project is licensed under the MIT License.

