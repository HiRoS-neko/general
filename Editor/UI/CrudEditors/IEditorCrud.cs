namespace Devdog.General.Editors
{
    public interface IEditorCrud
    {
        bool requiresDatabase { get; set; }

        void Focus();
        void Draw();
    }
}