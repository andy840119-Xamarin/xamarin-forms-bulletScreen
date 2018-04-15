using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BulletScreen.Model;
using Xamarin.Forms;

namespace BulletScreen.Controls
{
    /// <summary>
    /// Bullet screen view
    /// </summary>
    public class BulletScreenView : BulletScreenView<Comment,BulletScreenText>
    {

    }

    /// <summary>
    /// Bullet screen view
    /// </summary>
    /// <typeparam name="TComment"></typeparam>
    /// <typeparam name="TText"></typeparam>
    public class BulletScreenView<TComment,TText> : Layout<TText> , IBulletScreenView<TComment> where TComment : IComment where TText : View, IBulletScreenText , new()
    {
        #region Protected Property

        protected ObservableCollection<TComment> OnTimeComments { get;} = new ObservableCollection<TComment>();

        #endregion

        #region Public Property

        /// <summary>
        /// Text Opcity
        /// </summary>
        public double TextOpcity
        {
            get => this.Opacity;
            set => Opacity = value;
        }

        /// <summary>
        /// Cutrrent time
        /// </summary>
        public double CurrentTime { get; set; }

        /// <summary>
        /// Speed
        /// </summary>
        public double Speed { get; set; } = 1;

        /// <summary>
        /// Comments
        /// </summary>
        public ObservableCollection<TComment> Comments { get; set; } = new ObservableCollection<TComment>();

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BulletScreenView()
        {
            OnTimeComments.CollectionChanged += (a, b) =>
            {
                //new items add to the screen
                foreach (TComment newComment in b.NewItems ?? new List<TComment>())
                {
                    var text = CreateText(newComment);
                    Children.Add(text);
                }

                //old items will be remove to the screen
                foreach (TComment newComment in b.OldItems ?? new List<TComment>())
                {
                    var removeresult = Children.FirstOrDefault(X => X.CommentGuid == newComment.CommentGuid);

                    if (removeresult != null)
                        Children.Remove(removeresult);
                }
            };
        }

        #endregion

        #region Function

        /// <summary>
        /// update time
        /// </summary>
        /// <param name="currentTime"></param>
        protected void UpdateTime(double currentTime)
        {
            //text will stay at the screen tor 5 second
            var fakeTime = 5000;
            var inTimeList = Comments.Where(x => x.CommentTime < currentTime && (x.CommentTime - fakeTime) > currentTime).ToList();
            var nowComment = OnTimeComments.ToList();
            
            //https://stackoverflow.com/questions/12795882/quickest-way-to-compare-two-list
            var firstNotSecond = nowComment.Except(inTimeList).ToList();
            var secondNotFirst = inTimeList.Except(nowComment).ToList();

            //add to the list
            foreach (var comment in firstNotSecond)
            {
                OnTimeComments.Add(comment);
            }

            //remove from list
            foreach (var comment in secondNotFirst)
            {
                OnTimeComments.Remove(comment);
            }

            //update comment
            foreach (var comment in OnTimeComments)
            {
                var text = Children.FirstOrDefault(x => x.CommentGuid == comment.CommentGuid);
                UpdateProperty(comment, text, currentTime - comment.CommentTime);
            }
        }

        /// <summary>
        /// Layout child
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            //TODO : Guess maybe cal max row ?
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Create Text
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        protected TText CreateText(TComment comment)
        {
            var text = new TText();
            text.CommentGuid = comment.CommentGuid;
            text.Row = CalculateRow(OnTimeComments.ToList(), comment);
            InitialProperty(comment, text);
            return text;
        }

        #endregion

        #region Override Function

        /// <summary>
        /// Initial property
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="text"></param>
        protected virtual void InitialProperty(TComment comment, TText text)
        {
            if (text is Label)
            {
                var label = text as Label;
                
                label.Text = comment.Text;
                label.TextColor = Color.FromHex(comment.HexColor);
                label.FontSize = GetFontSizeByCommentTextSize(comment.Size);

                switch (comment.Orientation)
                {
                    case CommentOrientation.Top:
                        label.HorizontalTextAlignment = TextAlignment.Center;
                        break;
                    case CommentOrientation.Bottom:
                        label.HorizontalTextAlignment = TextAlignment.Center;
                        break;
                    case CommentOrientation.LeftToRight:
                        label.HorizontalTextAlignment = TextAlignment.End;
                        break;
                    case CommentOrientation.RightToLeft:
                        label.HorizontalTextAlignment = TextAlignment.Start;
                        break;
                }
            }
        }

        /// <summary>
        /// Calculate row
        /// </summary>
        /// <param name="OnScreenComments"></param>
        /// <param name="nowComment"></param>
        /// <returns></returns>
        protected virtual int CalculateRow(List<TComment> onScreenComments, TComment nowComment)
        {
            //TODO : implement
            return 0;
        }

        /// <summary>
        /// Get Y position by row number;
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected virtual int GetYPositionByRow(int row)
        {
            return row * 30;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="textAppearTime"></param>
        /// <returns></returns>
        protected virtual double GetXPositionByRelativeTime(TComment comment,double textAppearTime)
        {
            if (comment is ICommentMovingSpeed speedComment)
                return speedComment.Speed * Speed * textAppearTime;

            return this.Width / 2;
        }

        /// <summary>
        /// Get size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        protected virtual int GetFontSizeByCommentTextSize(CommentTextSize size)
        {
            switch (size)
            {
                case CommentTextSize.H1:
                    return 25;
                case CommentTextSize.H2:
                    return 20;
                case CommentTextSize.H3:
                    return 15;
                case CommentTextSize.H4:
                    return 12;
                case CommentTextSize.H5:
                    return 10;
                    default:
                        return 15;
            }
        }

        /// <summary>
        /// Update property
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="text"></param>
        /// <param name="textAppearTime"></param>
        protected virtual void UpdateProperty(TComment comment,TText text,double textAppearTime)
        {
            double xPosition = Width / 2;
            double yPosition = GetYPositionByRow(text.Row);
            switch (comment.Orientation)
            {
                case CommentOrientation.Top:

                    break;
                case CommentOrientation.Bottom:

                    break;
                case CommentOrientation.LeftToRight:
                    xPosition = Width - GetXPositionByRelativeTime(comment, textAppearTime);
                    break;
                case CommentOrientation.RightToLeft:
                    xPosition = GetXPositionByRelativeTime(comment, textAppearTime);
                    break;
            }

            //update object's position
            UpdateTextPosition(text, xPosition, yPosition);
        }

        /// <summary>
        /// Update position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected virtual void UpdateTextPosition(TText text, double x, double y)
        {
            var view = text as View;
            var request = view.Measure(this.Width, this.Height);
            var region = new Rectangle(x, y, request.Request.Width, request.Request.Height);
            LayoutChildIntoBoundingRegion(view, region);
        }

        #endregion
    }
}
