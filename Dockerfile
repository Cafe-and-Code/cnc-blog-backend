# ===========================================
# CNC Blog Backend Dockerfile
# For Coolify deployment (with Cloudflare Tunnel)
# ===========================================

FROM node:20-alpine

WORKDIR /app

# Install build dependencies for native modules (bcrypt)
RUN apk add --no-cache python3 make g++

# Copy package files
COPY package*.json ./

# Install dependencies (omit dev dependencies for smaller image)
RUN npm ci --omit=dev

# Clean up build dependencies
RUN apk del python3 make g++


# Copy source code
COPY . .

# Create images directory for uploads
RUN mkdir -p src/images && chmod 755 src/images

# Expose port
EXPOSE 3001

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD wget --no-verbose --tries=1 --spider http://localhost:3001/health || exit 1

# Start the application
CMD ["node", "src/server.js"]
