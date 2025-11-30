#!/bin/bash

# Check if year and day are provided
if [ -z "$1" ] || [ -z "$2" ]; then
  echo "Usage: $0 <year> <day>"
  exit 1
fi

YEAR=$1
DAY=$2
DAY_PADDED=$(printf "%02d" "$DAY")

# --- Configuration ---
SOLUTION_FILE="AocWebApp.sln"
SOLVERS_DIR="Solvers"
COMMON_PROJ_PATH="../../../Common/Common.csproj"

# --- Create Project ---
PROJ_NAME="Y${YEAR:2}-Day${DAY_PADDED}"
PROJ_DIR="${SOLVERS_DIR}/${YEAR}/${PROJ_NAME}"
PROJ_FILE="${PROJ_DIR}/${PROJ_NAME}.csproj"
PROJ_NAMESPACE="Y${YEAR:2}_Day${DAY_PADDED}"

mkdir -p "${PROJ_DIR}"
dotnet new classlib -n "${PROJ_NAME}" -o "${PROJ_DIR}" --force

# --- Add to Solution ---
dotnet sln "${SOLUTION_FILE}" add "${PROJ_FILE}"

# --- Add Reference to Common ---
(cd "${PROJ_DIR}" && dotnet add reference "${COMMON_PROJ_PATH}")

# --- Create Solver.cs ---
SOLVER_FILE="${PROJ_DIR}/Solver.cs"
cat > "${SOLVER_FILE}" << EOL
using Common.Solver;
using Common;
using Serilog;

namespace ${PROJ_NAMESPACE};

[Day(20${YEAR:2}, ${DAY}, "New Solver")]
public class Solver(string input, ILogger logger) : SolverBase(input, logger)
{
    public override Task Solve1()
    {
        logger.Information("Solving part 1...");
        // TODO: Solve part 1
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Solving part 2...");
        // TODO: Solve part 2
        return Task.CompletedTask;
    }
}
EOL

# --- Clean up ---
rm "${PROJ_DIR}/Class1.cs"

echo "Solver for Year ${YEAR}, Day ${DAY} created successfully."
echo "Project: ${PROJ_FILE}"
echo "Solver: ${SOLVER_FILE}"

