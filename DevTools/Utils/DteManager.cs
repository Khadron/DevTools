using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace KongQiang.DevTools.Utils
{
    public class DteManager
    {

        private DteManager()
        {
            _dte2 = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
            _dte2.Events.SelectionEvents.OnChange += new _dispSelectionEvents_OnChangeEventHandler(SelectionEvents_OnChange);
        }

        #region Singlton

        private static DteManager _Ide = new DteManager();

        public static DteManager Instance
        {
            get
            {
                return _Ide;
            }
        }
        #endregion

        #region Propertys

        private DTE2 _dte2 = null;

        public DTE2 DTE2
        {
            get
            {
                return _dte2;
            }
        }


        public Window ActiveWindow
        {
            get
            {
                CheckDTE();
                return _dte2.ActiveWindow;
            }
        }

        public Document ActiveDocument
        {
            get
            {
                CheckDTE();
                return _dte2.ActiveDocument;
            }
        }

        public IEnumerable<Project> ActiveProjects
        {
            get
            {
                CheckDTE();
                Array prjs = (Array)_dte2.ActiveSolutionProjects;
                foreach (var prj in prjs)
                {
                    yield return prj as Project;
                }

            }
        }

        public Project SelectedProject
        {
            get
            {
                if (ActiveProjects.Count() > 1)
                {
                    throw new Exception("Project is Multi-Selection");
                }
                return ActiveProjects.FirstOrDefault();
            }
        }

        public ToolWindows ToolWindows
        {
            get
            {
                CheckDTE();
                return _dte2.ToolWindows;
            }
        }

        /// <summary>
        /// 解决方案管理器中显示层次结构树数据的项
        /// </summary>
        public IEnumerable<UIHierarchyItem> ExplorerSelectedItems
        {
            get
            {
                CheckDTE();
                Array selectedItems = (Array)_dte2.ToolWindows.SolutionExplorer.SelectedItems;
                foreach (var item in selectedItems)
                {
                    yield return item as UIHierarchyItem;
                }

            }
        }

        public IEnumerable<UIHierarchyItem> ExplorerUiHierarchyItems
        {
            get
            {
                CheckDTE();
                var uiItems = _dte2.ToolWindows.SolutionExplorer.UIHierarchyItems;
                //var uiItems = _dte2.Solution.Projects;
                foreach (var item in uiItems)
                {
                    yield return item as UIHierarchyItem;
                }

            }
        }

        /// <summary>
        /// 返回选择的工程或工程中的项
        /// </summary>
        public IEnumerable<SelectedItem> SelectedItems
        {
            get
            {
                CheckDTE();
                SelectedItems selectedItems = _dte2.SelectedItems;
                foreach (var item in selectedItems)
                {
                    yield return item as SelectedItem;
                }

            }
        }

        public StatusBar StatusBar
        {
            get
            {
                CheckDTE();
                return _dte2.StatusBar;
            }
        }

        public OutputWindowPane OutputWindow
        {
            get
            {
                var outpanes = ToolWindows.OutputWindow.OutputWindowPanes;
                foreach (OutputWindowPane pane in outpanes)
                {
                    if (pane.Name.Equals("GLIde"))
                    {
                        return pane;
                    }
                }
                return ToolWindows.OutputWindow.OutputWindowPanes.Add("GLIde");
            }
        }

        public ErrorList ErrorWindow
        {
            get
            {
                return ToolWindows.ErrorList;
            }
        }

        private void CheckDTE()
        {
            if (_dte2 == null)
            {
                throw new Exception("DTE service not found");
            }
        }
        #endregion

        #region Events

        public event Action<object, EventArgs> SelectionChanged;

        void SelectionEvents_OnChange()
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }

        #endregion

    }

}
public static class Extentions
{
    public static void WriteLine(this OutputWindowPane outputPane, string context)
    {
        outputPane.OutputString(context + Environment.NewLine);
    }
}
