using System;
using Godot;

public partial class CameraControl : Camera3D
{
    [Export] public float MoveSpeed = 10f; // Movement speed
    [Export] public float RotateSpeed = 0.1f; // Rotation sensitivity
    [Export] public float ScrollSpeed = 5f; // Zoom speed

    private Vector2 _mouseDelta = Vector2.Zero; // Mouse movement delta

    public override void _Input(InputEvent @event)
    {

        // Handle mouse movement for rotation
        if (@event is InputEventMouseMotion mouseMotion)
        {
            _mouseDelta = mouseMotion.Relative;
        }

        // Handle mouse scroll for zoom
        if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.WheelUp)
        {
            Translate(Vector3.Forward * ScrollSpeed * (float) GetProcessDeltaTime());
        }
        else if (@event is InputEventMouseButton mouseButtonWheelDown && mouseButtonWheelDown.ButtonIndex == MouseButton.WheelDown)
        {
            Translate(Vector3.Back * ScrollSpeed * (float) GetProcessDeltaTime());
        }
    }

    public override void _Process(double delta)
    {
        // Handle keyboard input for movement
        Vector3 direction = Vector3.Zero;
        if (Input.IsActionPressed("move_forward")) direction += Transform.Basis.Y;
        if (Input.IsActionPressed("move_backward")) direction -= Transform.Basis.Y;
        if (Input.IsActionPressed("move_left")) direction -= Transform.Basis.X;
        if (Input.IsActionPressed("move_right")) direction += Transform.Basis.X;
        if (Input.IsActionPressed("move_up")) direction += Transform.Basis.Z;
        if (Input.IsActionPressed("move_down")) direction -= Transform.Basis.Z;

        // Normalize direction to maintain consistent speed
        direction = direction.Normalized();
        Translate(direction * MoveSpeed * (float) delta);

        // Handle mouse look for rotation
        if (Input.IsActionPressed("rotate_cam"))
        {
            RotateObjectLocal(Vector3.Up, -_mouseDelta.X * RotateSpeed);
            RotateObjectLocal(Vector3.Left, -_mouseDelta.Y * RotateSpeed);

            // Reset mouse delta each frame
            _mouseDelta = Vector2.Zero;
        }
    }
}