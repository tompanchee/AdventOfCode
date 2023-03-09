namespace Day11;

public class State
{
    readonly int elevatorFloor;

    public State(Floor[] floors, int elevatorFloor = 0) {
        Floors = floors;
        this.elevatorFloor = elevatorFloor;
    }

    public Floor[] Floors { get; }

    public long GetHash() {
        var hash = 0L;
        for (var i = 0; i < 4; i++) {
            var tmp = (long) Floors[i].GetHash() << (16 * i);
            hash += tmp;
        }

        hash <<= 2;
        hash |= (uint) elevatorFloor;

        return hash;
    }

    public IEnumerable<State> GetNextAllowedStates() {
        // Improve performance with more aggressive pruning
        var newStates = new List<State>();
        var currentFloor = Floors[elevatorFloor];

        var toMove = new List<Item[]>();
        for (var i = 0; i < currentFloor.Items.Count; i++) {
            toMove.Add(new[] {currentFloor.Items[i]});
            for (var j = i + 1; j < currentFloor.Items.Count; j++) toMove.Add(new[] {currentFloor.Items[i], currentFloor.Items[j]});
        }

        // Move up
        var newElevator = elevatorFloor + 1;
        if (newElevator < 4) // Check for allowed floor
        {
            foreach (var items in toMove) {
                var oldFloor = new Floor();
                var newFloor = new Floor();
                foreach (var item in Floors[newElevator].Items) newFloor.Items.Add(item);
                foreach (var item in items) newFloor.Items.Add(item);

                foreach (var item in currentFloor.Items.Except(items)) oldFloor.Items.Add(item);

                if (oldFloor.IsAllowed() && newFloor.IsAllowed()) {
                    var newFloors = new List<Floor>();
                    for (var i = 0; i < Floors.Length; i++)
                        if (i == elevatorFloor) {
                            newFloors.Add(oldFloor);
                        } else if (i == newElevator) {
                            newFloors.Add(newFloor);
                        } else {
                            newFloors.Add(Floors[i]);
                        }

                    newStates.Add(new State(newFloors.ToArray(), newElevator));
                }
            }
        }

        // Move down
        newElevator = elevatorFloor - 1;
        if (newElevator >= 0 && ShouldMoveDown()) // Check for allowed floor and prune stupid moves
        {
            foreach (var items in toMove) {
                var oldFloor = new Floor();
                var newFloor = new Floor();
                foreach (var item in Floors[newElevator].Items) newFloor.Items.Add(item);
                foreach (var item in items) newFloor.Items.Add(item);

                foreach (var item in currentFloor.Items.Except(items)) oldFloor.Items.Add(item);

                if (oldFloor.IsAllowed() && newFloor.IsAllowed()) {
                    var newFloors = new List<Floor>();
                    for (var i = 0; i < Floors.Length; i++)
                        if (i == elevatorFloor) {
                            newFloors.Add(oldFloor);
                        } else if (i == newElevator) {
                            newFloors.Add(newFloor);
                        } else {
                            newFloors.Add(Floors[i]);
                        }

                    newStates.Add(new State(newFloors.ToArray(), newElevator));
                }
            }
        }

        return newStates;

        bool ShouldMoveDown() {
            switch (elevatorFloor) {
                case 1 when !Floors[0].Items.Any():
                case 2 when !Floors[0].Items.Any() && !Floors[1].Items.Any():
                    return false;
                default:
                    return true;
            }
        }
    }
}