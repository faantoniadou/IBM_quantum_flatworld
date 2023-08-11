'''
This script generates PNG files for each gate in the gates list.
The PNG files are saved in the same directory as this script.
'''
import qiskit
from qiskit.visualization import plot_histogram, plot_bloch_vector, plot_gate_map, plot_circuit_layout
from qiskit.visualization import plot_state_city, plot_state_qsphere, plot_state_paulivec, plot_state_hinton
from matplotlib import pyplot as plt

# Function to save gate as PNG
def save_gate_as_png(gate_name):
    qc = qiskit.QuantumCircuit(1)
    
    if gate_name == 'h':
        qc.h(0)
    elif gate_name == 'x':
        qc.x(0)
    elif gate_name == 'y':
        qc.y(0)
    elif gate_name == 'z':
        qc.z(0)
    elif gate_name == 'cx':
        qc = qiskit.QuantumCircuit(2)
        qc.cx(0, 1)
    elif gate_name == 'ccx':
        qc = qiskit.QuantumCircuit(3) # Toffoli gate requires 3 qubits
        qc.ccx(0, 1, 2)
    elif gate_name == 'swap':
        qc = qiskit.QuantumCircuit(2)
        qc.swap(0, 1)
    elif gate_name == 'cz':
        qc = qiskit.QuantumCircuit(2)
        qc.cz(0, 1)
    elif gate_name == 'cy':
        qc = qiskit.QuantumCircuit(2)
        qc.cy(0, 1)
    elif gate_name == 'ch':
        qc = qiskit.QuantumCircuit(2)
        qc.ch(0, 1)
    elif gate_name == 'crx':
        qc = qiskit.QuantumCircuit(3) 
        qc.crx(0, 1, 2)
    elif gate_name == 'cry':
        qc = qiskit.QuantumCircuit(3) 
        qc.cry(0, 1, 2)
    elif gate_name == 'crz':
        qc = qiskit.QuantumCircuit(3) 
        qc.crz(0, 1, 2)
    elif gate_name == 'r':
        qc = qiskit.QuantumCircuit(3)
        qc.r(0, 1, 2)
    elif gate_name == 'rx':
        qc = qiskit.QuantumCircuit(2)
        qc.rx(0, 1)
    elif gate_name == 'ry':
        qc = qiskit.QuantumCircuit(2)
        qc.ry(0, 1)
    elif gate_name == 'rz':
        qc = qiskit.QuantumCircuit(2)
        qc.rz(0, 1)
    else:
        raise ValueError(f"Unknown gate: {gate_name}")
    
    figure = qc.draw(output='mpl')
    figure.savefig(f"{gate_name}.png")
    plt.close(figure)

# List of gates you want to generate icons for
gates = ['h', 'x', 'y', 'z', 'cx', 'ccx', 'swap', 'cz', 'cy', 'ch', 
         'crx', 'cry', 'crz', 'r', 'rx', 'ry', 'rz']

# Generate and save PNG for each gate
for gate in gates:
    save_gate_as_png(gate)
